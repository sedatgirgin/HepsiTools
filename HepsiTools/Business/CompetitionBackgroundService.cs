using HepsiTools.Business.Abstract;
using HepsiTools.Business.Concrate;
using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Abstract;
using HepsiTools.GenericRepositories.Concrate;
using HepsiTools.Helper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using HepsiTools.Models;

namespace HepsiTools.Business
{
    public class CompetitionBackgroundService : BackgroundService
    {
        private readonly ILogger<CompetitionBackgroundService> _logger;
        private System.Timers.Timer _timer;

        public CompetitionBackgroundService(IServiceProvider services,
            ILogger<CompetitionBackgroundService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service running.");

            _timer = new System.Timers.Timer();
            _timer.Enabled = true;
            _timer.Interval = 30000;
            _timer.Elapsed += Handler;
        }

        private async void Handler(object sender, System.Timers.ElapsedEventArgs args)
        {
            ICompetitionAnalsesHistoryRepository _competitionAnalsesHistoryRepository = new CompetitionAnalsesHistoryRepository();
            ICompanyRepository _companyRepository = new CompanyRepository();
            ICompetitionAnalysesRepository _competitionAnalysesRepository = new CompetitionAnalysesRepository();
            ILisansRepository _lisansRepository = new LisansRepository();
            IErrorRepository _errorRepository = new ErrorRepository();

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";

            try
            {
                var userLisans = _lisansRepository.GetList(i => i.IsActive == true && i.EndDate < DateTime.Now);

                foreach (var item in userLisans)
                {
                    item.IsActive = false;
                    _lisansRepository.UpdateAsync(item);

                    var expireLisansCompetitionList = _competitionAnalysesRepository.GetCompetitionsByUserAsync(item.UserId);

                    foreach (var competition in expireLisansCompetitionList)
                    {
                        competition.StatusType = StatusType.Cancel;
                        _competitionAnalysesRepository.UpdateAsync(competition);
                        _competitionAnalsesHistoryRepository.InsertAsync(new CompetitionAnalysesHistory()
                        {
                            HistoryType = HistoryType.StatusChange,
                            CompetitionAnalysesId = competition.Id,
                            Note = "Lisans süresi dolduğu için işlem İPAL edildi."
                        });
                    }
                }

                var endDateCompletedAnalysesList = _competitionAnalysesRepository.GetList(i => i.StatusType != StatusType.Cancel && i.EndDate < DateTime.Now);

                foreach (var item in endDateCompletedAnalysesList)
                {
                    item.StatusType = StatusType.Cancel;
                    _competitionAnalysesRepository.UpdateAsync(item);
                    _competitionAnalsesHistoryRepository.InsertAsync(new CompetitionAnalysesHistory()
                    {
                        HistoryType = HistoryType.StatusChange,
                        CompetitionAnalysesId = item.Id,
                        Note = "Bitiş süresi dolduğu için işlem İPAL edildi."
                    });
                }

                var analysesList = _competitionAnalysesRepository.GetList(i => i.StatusType != StatusType.Cancel && i.StartDate < DateTime.Now && i.EndDate > DateTime.Now);

                foreach (var item in analysesList)
                {
                    using (WebClient client = new WebClient())
                    {
                        var uri = new Uri(item.ParserLink);
                        var host = uri.Host;
                        var scheme = uri.Scheme; 

                        client.Encoding = Encoding.UTF8;

                        string html = client.DownloadString(item.ParserLink); 

                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(html);

                        HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='prdct-desc-cntnr-ttl-w two-line-text']");
                        HtmlNodeCollection htmlNodes2 = htmlDocument.DocumentNode.SelectNodes("//div[@class='prc-box-dscntd']");

                        double _salePrice = 0.0;
                        for (int i = 0; i < htmlNodes.Count; i++)
                        {
                            if (htmlNodes[i].InnerText.Contains(item.ProductInfo))
                            {
                                _salePrice = Convert.ToDouble(htmlNodes2[i].InnerText.Split(" ")[0], provider);
                                htmlNodes.Remove(i);
                            }
                        }

                        if (_salePrice != 0.0)
                        {
                            for (int i = 0; i < htmlNodes2.Count; i++)
                            {
                                var price = Convert.ToDouble(htmlNodes2[i].InnerText.Split(" ")[0], provider);

                                if (_salePrice > price)
                                {
                                    if (item.LowestPrice < price - item.Multiple)
                                    {
                                        item.SalePrice = price - item.Multiple;
                                        _competitionAnalysesRepository.Update(item);
                                        _competitionAnalsesHistoryRepository.Insert(new CompetitionAnalysesHistory()
                                        {
                                            HistoryType = HistoryType.PriceChange,
                                            OldPrice = _salePrice,
                                            NewPrice = item.SalePrice,
                                            CompetitionAnalysesId = item.Id,
                                            Note = $"Fiyat değişikliği yeni fiyat: {item.SalePrice} eski fiyat: {item.SalePrice}"
                                        });
                                       var company =  _companyRepository.Get(item.CompanyId);

                                        string trendyolApi = "https://api.trendyol.com/sapigw/suppliers/{0}/products/price-and-inventory";
                                        string targetUrl = String.Format(trendyolApi, company.SupplierId);

                                        HttpClient client2 = new HttpClient();
                                        var byteArray = Encoding.ASCII.GetBytes(String.Format("{0}:{1}", company.UserName, company.Password));
                                        client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                                        client2.DefaultRequestHeaders.Add("User-Agent", "200300444 - Trendyolsoft");
                                        client2.DefaultRequestHeaders.Add("Content-Type", "application/json");


                                        var newPrice = new UpdatePriceModel(item.Product, _salePrice , _salePrice);

                                        var json = JsonConvert.SerializeObject(newPrice);
                                        var data = new StringContent(json, Encoding.UTF8, "application/json");

                                        HttpResponseMessage response = await client2.PostAsync(targetUrl, data);
                                    }
                                }
                            }
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(nameof(CompetitionBackgroundService) + "  " + ex.Message);
                _errorRepository.Insert(new ErrorLog { Message = ex.Message, Source = nameof(CompetitionBackgroundService), StackTrace = ex.StackTrace });
            }
          
        }

        public void StartPublishing()
        {
            _timer.Start();
        }

        public void StopPublishing()
        {
            _timer.Stop();
        }

    }
}
