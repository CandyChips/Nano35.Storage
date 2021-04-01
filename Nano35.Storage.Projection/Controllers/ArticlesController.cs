using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nano35.Storage.Projection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        
        public ArticlesController(IServiceProvider services) { _services = services; }
    
        [HttpGet]
        [Route("GetAllArticles")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllArticles()
        {
            return Ok();
        }
         
    }
}