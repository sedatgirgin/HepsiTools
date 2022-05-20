using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiTools.Entities
{
    public class UserLisans
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Lisans Lisans { get; set; }
        public int LisansId { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);

    }
}
