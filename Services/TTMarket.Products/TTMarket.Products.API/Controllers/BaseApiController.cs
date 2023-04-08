using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TTMarket.Products.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender;
        protected ISender Sender 
            => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}