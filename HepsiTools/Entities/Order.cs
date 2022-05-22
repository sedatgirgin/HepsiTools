﻿namespace HepsiTools.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int WooOrderId { get; set; }
        public string Note { get; set; }
        public int WooCommerceDataId { get; set; }
        public WooCommerceData WooCommerceData { get; set; }
    }
}
