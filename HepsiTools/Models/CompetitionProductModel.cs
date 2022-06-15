using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiTools.Models
{
    public class Attribute
    {
        public int attributeId { get; set; }
        public string attributeName { get; set; }
        public string attributeValue { get; set; }
        public int attributeValueId { get; set; }
    }

    public class Content
    {
        public bool approved { get; set; }
        public bool archived { get; set; }
        public List<Attribute> attributes { get; set; }
        public string barcode { get; set; }
        public string batchRequestId { get; set; }
        public string brand { get; set; }
        public int brandId { get; set; }
        public string categoryName { get; set; }
        public string description { get; set; }
        public bool hasActiveCampaign { get; set; }
        public string id { get; set; }
        public List<Image> images { get; set; }
        public int listPrice { get; set; }
        public string lockReason { get; set; }
        public bool locked { get; set; }
        public bool onSale { get; set; }
        public int pimCategoryId { get; set; }
        public string platformListingId { get; set; }
        public int productCode { get; set; }
        public int productContentId { get; set; }
        public string productMainId { get; set; }
        public int quantity { get; set; }
        public int salePrice { get; set; }
        public int shipmentAddressId { get; set; }
        public string stockCode { get; set; }
        public string stockId { get; set; }
        public string stockUnitType { get; set; }
        public int supplierId { get; set; }
        public string title { get; set; }
        public int vatRate { get; set; }
        public int version { get; set; }
        public bool rejected { get; set; }
        public bool blacklisted { get; set; }
        public int? dimensionalWeight { get; set; }
        public string blacklistReason { get; set; }
        public string gender { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
    }

    public class CompetitionProductModel
    {
        public int page { get; set; }
        public int size { get; set; }
        public int totalElements { get; set; }
        public int totalPages { get; set; }
        public List<Content> content { get; set; }
    }
}
