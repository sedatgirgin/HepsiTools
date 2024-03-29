﻿using HepsiTools.Helper;
using System;
using System.Collections.Generic;

namespace HepsiTools.Entities
{
    public class CompetitionAnalyses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double HighestPrice { get; set; }
        public double LowestPrice { get; set; }
        public double Multiple { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddHours(3);
        public string Product { get; set; }
        public string ProductLink { get; set; }
        public string ProductInfo { get; set; }
        public string Note { get; set; }
        public string RepetitionCount { get; set; }
        public double SalePrice { get; set; }
        public string ParserLink { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public StatusType StatusType { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<CompetitionAnalysesHistory> CompetitionAnalysesHistories { get; set; }
    }
}
