using AutoMapper;
using HepsiTools.Business.Abstract;
using HepsiTools.Entities;
using HepsiTools.Models;
using HepsiTools.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IMapper _mapper;
        private readonly IWooCommerceDataRepository _wooCommerceDataRepository;
        private readonly IConfiguration _config;

        public WooCommerceController(IConfiguration config, IWooCommerceDataRepository wooCommerceDataRepository, IMapper mapper)
        {
            _wooCommerceDataRepository = wooCommerceDataRepository;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(WooCommerceInsertModel model)
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


        [HttpGet("Update")]
        public async Task<IActionResult> UpdateAsync(WooCommercenModel model)
        {

            if (!ModelState.IsValid)
                return new ErrorResult("Hatalı istek", BadRequest(ModelState).Value);

            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _wooCommerceDataRepository.Get(i => i.UserId == _userId && i.Id == model.Id);
            if (result != null)
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    result.Name = model.Name;
                }
                if (!string.IsNullOrEmpty(model.Consumer_key))
                {
                    result.Consumer_key = model.Consumer_key;
                }
                if (!string.IsNullOrEmpty(model.Consumer_secret))
                {
                    result.Consumer_secret = model.Consumer_secret;
                }
                if (!string.IsNullOrEmpty(model.StoreURL))
                {
                    result.StoreURL = model.StoreURL;
                }

                _wooCommerceDataRepository.Update(result);
                return new Result("Başarılı", result);
            }
            return new ErrorResult("Hatalı istek");
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteAsync(int wooCommerceId)
        {
            if (wooCommerceId == 0)
                return new ErrorResult("Hatalı istek", BadRequest());

            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _wooCommerceDataRepository.Get(i => i.UserId == _userId && i.Id == wooCommerceId);
            if (result != null)
            {
                _wooCommerceDataRepository.Delete(result);
                return new Result("Başarılı");
            }
            return new ErrorResult("Hatalı istek");
        }


        [HttpGet("GetList")]
        public async Task<IActionResult> GetListAsync()
        {
            var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _wooCommerceDataRepository.GetList(i => i.UserId == _userId);

            return new Result("Başarılı", result);
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

            string targetUrl = result.StoreURL + _config["WooCommerceUrl"];
            RestAPI restClient = new RestAPI(targetUrl, result.Consumer_key, result.Consumer_secret);

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
