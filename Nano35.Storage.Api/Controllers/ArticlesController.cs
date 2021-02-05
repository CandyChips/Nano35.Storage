using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateArticle;
using Nano35.Storage.Api.Requests.CreateCategory;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleCategories;
using Nano35.Storage.Api.Requests.GetAllArticleModels;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetArticleById;

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
        [Route("GetAllArticleModels")]
        public async Task<IActionResult> GetAllArticleModels(
            [FromQuery] Guid instanceId)
        {
            var request = new GetAllArticlesModelsQuery() {InstanceId = instanceId};
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllArticlesModelsSuccessResultContract success => Ok(success.Data),
                IGetAllArticlesModelsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllArticleBrands")]
        public async Task<IActionResult> GetAllArticleBrands(
            [FromQuery] Guid instanceId)
        {
            var request = new GetAllArticleBrandsQuery() {InstanceId = instanceId};
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllArticlesBrandsSuccessResultContract success => Ok(success.Data),
                IGetAllArticlesBrandsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllArticleCategories")]
        public async Task<IActionResult> GetAllArticleCategories(
            [FromQuery] Guid instanceId,
            [FromQuery] Guid parentId)
        {
            var request = new GetAllArticlesCategoriesQuery() {InstanceId = instanceId, ParentId = parentId};
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllArticlesCategoriesSuccessResultContract success => Ok(success.Data),
                IGetAllArticlesCategoriesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetArticleById")]
        public async Task<IActionResult> GetArticleById(
            [FromQuery] Guid id)
        {
            var request = new GetArticleByIdQuery()
            {
                Id = id
            };
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetArticleByIdSuccessResultContract success => Ok(success.Data),
                IGetArticleByIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
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

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);

            return result switch
            {
                ICreateCategorySuccessResultContract => Ok(),
                ICreateCategoryErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}