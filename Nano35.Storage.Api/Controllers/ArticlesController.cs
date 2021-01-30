using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(
            ILogger<ArticlesController> logger)
        {
            _logger = logger;
        }
    
        [HttpGet]
        [Route("GetAllArticles")]
        public async Task<IActionResult> GetAllArticles()
        {
            return Ok();
        }
    
        [HttpGet]
        [Route("GetAllArticleTypes")]
        public async Task<IActionResult> GetAllClientTypes()
        {
            return Ok();
        }
    
        [HttpGet]
        [Route("GetAllArticleModels")]
        public async Task<IActionResult> GetAllArticleModels()
        {
            return Ok();
        }
    
        [HttpGet]
        [Route("GetAllArticleBrands")]
        public async Task<IActionResult> GetAllArticleBrands()
        {
            return Ok();
        }
    
        [HttpGet]
        [Route("GetAllArticleCategories")]
        public async Task<IActionResult> GetAllArticleCategories()
        {
            return Ok();
        }
    
        [HttpGet]
        [Route("GetAllArticleCategoryGroups")]
        public async Task<IActionResult> GetAllArticleCategoryGroups()
        {
            return Ok();
        }

        [HttpPost]
        [Route("CreateArticle")]
        public async Task<IActionResult> CreateArticle()
        {
            return Ok();
        }

        [HttpPut]
        [Route("UpdateArticle")]
        public async Task<IActionResult> UpdateArticle()
        {
            return Ok();
        }
    }
}