using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IMediator _mediator;

        public ArticlesController(
            ILogger<ArticlesController> logger, 
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    
        [HttpGet]
        [Route("GetAllArticles")]
        public async Task<IActionResult> GetAllArticles(
            [FromQuery] Guid instanceId)
        {
            var request = new GetAllArticlesQuery()
            {
                InstanceId = instanceId
            };
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllArticlesSuccessResultContract success => Ok(success.Data),
                IGetAllArticlesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
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
        public async Task<IActionResult> CreateArticle(
            [FromBody] CreateArticleCommand command)
        {
            var result = await _mediator.Send(command);

            return result switch
            {
                ICreateArticleSuccessResultContract => Ok(),
                ICreateArticleErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdateArticle")]
        public async Task<IActionResult> UpdateArticle()
        {
            return Ok();
        }
    }
}