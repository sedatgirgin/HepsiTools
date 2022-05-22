namespace HepsiTools.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int WooProductId { get; set; }
        public string Note { get; set; }
        public int WooCommerceDataId { get; set; }
        public WooCommerceData WooCommerceData { get; set; }

    }
}
