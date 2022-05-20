using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiTools.Entities
{
    public class Lisans
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserLisans> UserLisans { get; set; }

    }
}
