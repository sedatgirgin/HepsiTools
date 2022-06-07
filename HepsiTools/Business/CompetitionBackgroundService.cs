using HepsiTools.Business.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HepsiTools.Business
{
    public class CompetitionBackgroundService : IHostedService, IDisposable
    {
        private readonly ICompetitionAnalsesHistoryRepository _competitionAnalsesHistoryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompetitionAnalysesRepository _competitionAnalysesRepository;
        private Timer _timer = null;

        public CompetitionBackgroundService(ICompetitionAnalsesHistoryRepository competitionAnalsesHistoryRepository, ICompanyRepository companyRepository, ICompetitionAnalysesRepository competitionAnalysesRepository)
        {
            _competitionAnalsesHistoryRepository = competitionAnalsesHistoryRepository;
            _companyRepository = companyRepository;
            _competitionAnalysesRepository = competitionAnalysesRepository;
        }
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,TimeSpan.FromMinutes(15));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var data = _competitionAnalysesRepository.GetList(i => i.StatusType != Helper.StatusType.Cancel && i.StartDate > DateTime.Now && i.EndDate < DateTime.Now);

            while (data.Count > 0)
            {
                //lisansıda kontrol et ki lisansı biten kişini işlemini hala yapıyor olmayalım
                //html parser yaz
                //fiyat deglştirme algorritması ekle
                //history tablosunu doldur
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
