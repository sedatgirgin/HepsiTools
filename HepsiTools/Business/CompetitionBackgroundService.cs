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
            _timer.Interval = 10000;
            _timer.Elapsed += Handler;
        }

        private void Handler(object sender, System.Timers.ElapsedEventArgs args)
        {
            ICompetitionAnalsesHistoryRepository _competitionAnalsesHistoryRepository = new CompetitionAnalsesHistoryRepository();
            ICompanyRepository _companyRepository = new CompanyRepository();
            ICompetitionAnalysesRepository _competitionAnalysesRepository = new CompetitionAnalysesRepository();
            ILisansRepository _lisansRepository = new LisansRepository();
            IErrorRepository _errorRepository = new ErrorRepository();

            try
            {
                var userLisans = _lisansRepository.GetList(i => i.IsActive == true && i.EndDate < DateTime.Now);

                foreach (var item in userLisans)
                {
                    item.IsActive = false;
                    _lisansRepository.Update(item);

                    var expireLisansCompetitionList = _competitionAnalysesRepository.GetCompetitionsByUserAsync(item.UserId);

                    foreach (var competition in expireLisansCompetitionList)
                    {
                        competition.StatusType = StatusType.Cancel;
                        _competitionAnalysesRepository.Update(competition);
                        _competitionAnalsesHistoryRepository.Insert(new CompetitionAnalysesHistory()
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
                    _competitionAnalysesRepository.Update(item);
                    _competitionAnalsesHistoryRepository.Insert(new CompetitionAnalysesHistory()
                    {
                        HistoryType = HistoryType.StatusChange,
                        CompetitionAnalysesId = item.Id,
                        Note = "Bitiş süresi dolduğu için işlem İPAL edildi."
                    });
                }



                var analysesList = _competitionAnalysesRepository.GetList(i => i.StatusType != StatusType.Cancel && i.StartDate < DateTime.Now && i.EndDate > DateTime.Now);

                foreach (var item in analysesList)
                {
                    //HttpClient client = new HttpClient();
                    //HttpResponseMessage response = client.GetAsync(item.ProductLink).Result;
                    //string result = response.Content.ReadAsStringAsync().Result;
                    using (WebClient client = new WebClient()) // Html'i indirmek için bir İstemci Oluşturuyoruz.
                    {
                        var uri = new Uri(item.ProductLink); // link yazan alana verisini okumak istediğimiz web sayfasının bağlantısını yazıyoruz.
                        var host = uri.Host; // bu kısım verdiğimiz linkin Base Url (Merkez Bağlantısı Örn: "http://www.fatihbas.net/2019/04/19/cpu-sicakligi/" adresinden bize sadece "www.fatihbas.net" i döndürüyor)'ni döndürüyor.
                        var scheme = uri.Scheme; // bu kısım ise girmiş olduğumuz linkin "HTTP" veya "HTTPS" olup olmadığını döndürüyor.

                        client.Encoding = Encoding.UTF8; // sayfa kodlama karakter ailesinin UTF-8 olduğunu belirtiyoruz (%90 web sayfaları UTF-8 Olarak kodlanmaktadır)

                        string html = client.DownloadString(item.ProductLink); // bu satırda ise web sayfasının içeriğini indiriyoruz.

                        // Bir HtmlDocument Oluşturarak indirmiş olduğumuz HTML ifadesini içerisine yüklüyoruz.
                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(html);

                        // artık parse işlemine geçebiliriz

                        HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='wd-single-post no-thumb']"); // burada dikkat edilmesi gereken bir konu, ben birden fazla elemanı döndürmek istediğim için class'ı kullandım. gelen ifade düz metin olduğu için jQuery gibi gelişmiş değil dolayısı ile elementin sahip olduğu class'ı olduğu gibi veriyorum. Burada www.fatihbas.net i baz alarak size örneklendirmeye devam edeceğim

                        if (htmlNodes != null)
                        {
                            //bu alanda istediğimiz sonuçları okumaya yakın olduğumuzu görüyoruz.
                            foreach (HtmlNode node in htmlNodes)
                            {
                                // son olarak gelen elemanları sırası ile burada okuyacağız.
                                HtmlAgilityPack.HtmlDocument _subDocument = new HtmlAgilityPack.HtmlDocument();
                                _subDocument.LoadHtml(node.InnerHtml);
                                // Gelen nesnemizin alt elemanlarını okurken sorun yaşamamız için yeni bir HtmlDocument oluşturuyoruz.
                                // Ve artık istediğimiz dataları okuyabiliriz.
                                HtmlNode linkNode = _subDocument.DocumentNode.SelectSingleNode("//a[@class='more-link']");
                                // linkNode Değişkeninde sayfamda bulunan "Devamını Oku (Read More)" butonunu getirmiş oldum.
                                string devaminiOkuLink = linkNode.Attributes["href"].Value; // Devamını Oku butonunun içerisinde ki bağlantıyı almış oldum.

                                HtmlNode icerikNode = _subDocument.DocumentNode.SelectSingleNode("//div[@class='wd-excerpt-content']");

                                string icerik = icerikNode.InnerHtml; // iceriklerimi HTML Olarak alıyorum. Yanlış Hatırlamıyorsam .InnerHtml yerine ".InnerText" yazarak direkt olarak HTML etiketleri (tag) olmadan salt metni alabilirsiniz. 
                            }
                        }
                    }
                    //html parser yaz
                    //fiyat deglştirme algorritması ekle

                    _competitionAnalsesHistoryRepository.Insert(new CompetitionAnalysesHistory()
                    {
                        HistoryType = HistoryType.PriceChange,
                        OldPrice = 5,
                        NewPrice = 10,
                        CompetitionAnalysesId = item.Id,
                        Note = $"Fiyat değişikliği yeni fiyat: {item.SalePrice} eski fiyat: {item.SalePrice}"
                    });
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
