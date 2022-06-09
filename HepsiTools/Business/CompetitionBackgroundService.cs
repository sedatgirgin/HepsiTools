using HepsiTools.Business.Abstract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service running.");

            _timer = new System.Timers.Timer();
            _timer.Enabled = true;
            _timer.Interval = 90000;
            _timer.Elapsed += Handler;
        }

        private void Handler(object sender, System.Timers.ElapsedEventArgs args)
        {
            using (var scope = Services.CreateScope())
            {
                var _competitionAnalsesHistoryRepository =
                    scope.ServiceProvider
                        .GetRequiredService<ICompetitionAnalsesHistoryRepository>();


                var _companyRepository =
                 scope.ServiceProvider
                     .GetRequiredService<ICompanyRepository>();


                var _competitionAnalysesRepository =
                 scope.ServiceProvider
                     .GetRequiredService<ICompetitionAnalysesRepository>();

                var _LisansRepository =
                scope.ServiceProvider
                    .GetRequiredService<ILisansRepository>();

                var lisansList = _LisansRepository.GetList(i => i.IsActive == true && i.EndDate < DateTime.Now);


                var data = _competitionAnalysesRepository.GetList(i => i.StatusType != Helper.StatusType.Cancel && i.StatusType != Helper.StatusType.TimeIsUp && i.StartDate > DateTime.Now && i.EndDate < DateTime.Now);

                while (data.Count > 0)
                {

                    //lisansıda kontrol et ki lisansı biten kişini işlemini hala yapıyor olmayalım
                    //html parser yaz
                    //fiyat deglştirme algorritması ekle
                    //history tablosunu doldur
                }
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
