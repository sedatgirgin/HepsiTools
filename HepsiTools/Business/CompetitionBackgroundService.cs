using HepsiTools.Business.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace HepsiTools.Business
{
    public class CompetitionBackgroundService : BackgroundService
    {
        private readonly ICompetitionAnalsesHistoryRepository _competitionAnalsesHistoryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompetitionAnalysesRepository _competitionAnalysesRepository;

        public CompetitionBackgroundService(ICompetitionAnalsesHistoryRepository competitionAnalsesHistoryRepository, ICompanyRepository companyRepository, ICompetitionAnalysesRepository competitionAnalysesRepository)
        {
            _competitionAnalsesHistoryRepository = competitionAnalsesHistoryRepository;
            _companyRepository = companyRepository;
            _competitionAnalysesRepository = competitionAnalysesRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            SetTimer();

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await base.StopAsync(stoppingToken);
        }

        public override async Task StartAsync(CancellationToken stoppingToken)
        {
            await base.StartAsync(stoppingToken);
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            System.Timers.Timer aTimer = new System.Timers.Timer(900);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            var data = _competitionAnalysesRepository.GetList(i => i.StatusType != Helper.StatusType.Cancel && );

            while (data.Count > 0)
            {



            }
        }

    }
}
