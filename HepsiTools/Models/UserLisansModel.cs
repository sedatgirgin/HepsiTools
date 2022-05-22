﻿using System;

namespace HepsiTools.Models
{
    public class UserLisansModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LisansId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);

    }
}
