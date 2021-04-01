using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nano35.Storage.Projection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        
        public CategoryController(IServiceProvider services) { _services = services; }
    
        [HttpGet]
        [Route("GetAllCategories")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllArticleCategories()
        {
            return Ok();
        }
    }
}