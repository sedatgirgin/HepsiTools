namespace HepsiTools.Models
{
    public class UpdatePriceModel
    {
        public UpdatePriceModel(string barcode, double salePrice, double listPrice)
        {
            this.ListPrice = listPrice;
            this.SalePrice = salePrice;
            this.Barcode = barcode;
        }
        public string Barcode { get; set; }
        public double SalePrice { get; set; }
        public double ListPrice { get; set; }

    }
}
