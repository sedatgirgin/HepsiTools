using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiTools.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Lisans> Lisans { get; set; }
        public List<WooCommerceData> WooCommerceDatas { get; set; }
        public List<Company> Companys { get; set; }
    }
}
