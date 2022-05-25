using HepsiTools.Business.Abstract;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
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

        [HttpPost("AddWooCommerce")]
        public async Task<IActionResult> AddWooCommerceAsync(WooCommerceInsertModel model)
        {
            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _wooCommerceDataRepository.Insert(new Entities.WooCommerceData()
            {
                Consumer_key = model.Consumer_key,
                Consumer_secret = model.Consumer_secret,
                Name = model.Name,
                UserId = _userId,
                StoreURL = model.StoreURL
            });

            if (!result.Equals(null))
            {
                return new Result("Başarılı", result);
            }
            return new ErrorResult("Lütfen bilgilerinizi kontrol edin.");
        }


        [HttpGet("GetWooCommerces")]
        public async Task<IActionResult> GetWooCommercesAsync()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _wooCommerceDataRepository.GetList(i => i.UserId == _userId);

            return new Result("Başarılı", new { WooCommerces = result });
        }

        [HttpPost("GetOrders")]
        public async Task<IActionResult> GetOrdersAsync(int wooCommerceId)
        {
            if (wooCommerceId == 0)
                return new ErrorResult("Hatalı istek", BadRequest());

            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _wooCommerceDataRepository.Get(i => i.UserId == _userId && i.Id == wooCommerceId);

            if (result == null)
                return new ErrorResult("WooCommerce Not Found");

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
                    var data = products.Find(i => i.id == item.line_items[0].product_id);
                    if (data !=null)
                    {
                        response.Add(new { item, image = data.images[0].src });
                    }
                    else
                    {
                        response.Add(new { item, image = "" });
                    }
                }
            }

            return new Result("Başarılı", new { Orders = response });
        }
    }
}
