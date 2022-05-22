using System;

namespace HepsiTools.Entities
{
    public class CompetitionAnalyses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double HighestPrice { get; set; }
        public double LowestPrice { get; set; }
        public string Multiple { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddHours(3);
        public string Product { get; set; }
        public string Param { get; set; }
        public int ConnectionInfoId { get; set; }
        public ConnectionInfo ConnectionInfo { get; set; }
    }
}
