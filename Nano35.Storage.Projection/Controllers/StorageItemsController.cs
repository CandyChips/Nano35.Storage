using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nano35.Storage.Projection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public StorageItemsController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Route("GetAllStorageItems")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllStorageItems()
        {
            return Ok();
        }
    
        [HttpGet]
        [Route("GetAllStorageItemConditions")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllStorageItemConditions()
        {
            return Ok();
        }
    }
}