namespace HepsiTools.Models
{
    public class CompanyModel
    {
        public int Id { get; set; }
        public int CompanyType { get; set; }
        public string SupplierId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CargoCompanyId { get; set; }
        public string CustomResourceName { get; set; }
    }
}
