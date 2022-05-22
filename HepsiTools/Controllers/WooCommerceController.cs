using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HepsiTools.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WooCommerceController : ControllerBase
    {

        public WooCommerceController()
        {

        }
    }
}
