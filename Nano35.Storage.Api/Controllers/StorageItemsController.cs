using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemsController : ControllerBase
    {
        private readonly ILogger<StorageItemsController> _logger;

        public StorageItemsController(
            ILogger<StorageItemsController> logger)
        {
            _logger = logger;
        }
    
        [HttpGet]
        [Route("GetAllStorageItems")]
        public async Task<IActionResult> GetAllStorageItems()
        {
            return Ok();
        }

        [HttpPost]
        [Route("CreateStorageItem")]
        public async Task<IActionResult> CreateStorageItem()
        {
            return Ok();
        }

        [HttpPut]
        [Route("UpdateStorageItem")]
        public async Task<IActionResult> UpdateStorageItem()
        {
            return Ok();
        }
    }
}