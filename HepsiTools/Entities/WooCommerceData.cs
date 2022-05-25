using System.Collections.Generic;

namespace HepsiTools.Entities
{
    public class WooCommerceData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StoreURL { get; set; }
        public string Consumer_key { get; set; }
        public string Consumer_secret { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

    }
}
