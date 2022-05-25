using HepsiTools.Helper;
using System;

namespace HepsiTools.Entities
{
    public class CompetitionAnalysesHistory
    {
        public int Id { get; set; }
        public Nullable<double> OldPrice { get; set; }
        public Nullable<double> NewPrice { get; set; }
        public string Note { get; set; }        
        public HistoryType HistoryType { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int CompetitionAnalysesId { get; set; }
        public  CompetitionAnalyses CompetitionAnalyses { get; set; }
        
    }
}
