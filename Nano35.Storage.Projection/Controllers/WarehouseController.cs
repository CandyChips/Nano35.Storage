using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nano35.Storage.Projection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public WarehouseController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Route("GetAllStorageItemsOnInstance")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllStorageItemsOnInstance()
        {
            return Ok();
        }
    }
}