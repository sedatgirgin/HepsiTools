using HepsiTools.Business.Abstract;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;

namespace HepsiTools.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WooCommerceController : ControllerBase
    {
        private readonly IWooCommerceDataRepository _wooCommerceDataRepository;
        public WooCommerceController(IWooCommerceDataRepository wooCommerceDataRepository)
        {
            _wooCommerceDataRepository = wooCommerceDataRepository;
        }

        [HttpPost("InsertWooCommerce")]
        public async Task<IActionResult> InsertWooCommerceAsync(WooCommerceModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var result = _wooCommerceDataRepository.Insert(new Entities.WooCommerceData()
            {
                Consumer_key = model.Consumer_key,
                Consumer_secret = model.Consumer_secret,
                Name = model.Name,
                UserId = model.UserId,
                StoreURL = model.StoreURL
            });

            if (!result.Equals(null))
            {
                return new Result("Başarılı");
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }


        [HttpGet("GetWooCommerces")]
        public async Task<IActionResult> GetWooCommercesAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new ErrorResult("Hatalı istek");

            var result = _wooCommerceDataRepository.GetList(i => i.UserId == userId);

            return new Result("Başarılı", new { WooCommerces = result });
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrdersAsync(OrderModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var result = _wooCommerceDataRepository.Get(i => i.UserId == model.UserId && i.Id == model.WooCommerceId);

            if (result == null)
                return new ErrorResult("Hatalı istek");

            //https://naturalbastet.com/wp-json/wc/v3/
            RestAPI restClient = new RestAPI(result.StoreURL, result.Consumer_key, result.Consumer_secret);

            WCObject wCObject = new WCObject(restClient);
            var orders = wCObject.Order.GetAll().Result;
            var products = wCObject.Product.GetAll().Result;

            List<object> response = new List<object>();
            foreach (var item in orders)
            {
                if (item.line_items.Count>0 )
                {
                   var data = products.Find(i => i.id == item.line_items[0].product_id).images[0].src;
                    response.Add(new { item, image = data });
                }
            }

            return new Result("Başarılı", new { Orders = response });
        }
    }
}
