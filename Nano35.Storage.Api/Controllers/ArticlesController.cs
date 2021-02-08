using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateArticle;
using Nano35.Storage.Api.Requests.CreateCategory;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetAllArticlesBrands;
using Nano35.Storage.Api.Requests.GetAllArticlesCategories;
using Nano35.Storage.Api.Requests.GetAllArticlesModels;
using Nano35.Storage.Api.Requests.GetArticleById;
using Nano35.Storage.HttpContext;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IServiceProvider _services;

        public ArticlesController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        [HttpGet]
        [Route("GetAllArticles")]
        public async Task<IActionResult> GetAllArticles(
            [FromQuery] GetAllArticlesHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllArticlesLogger>)_services.GetService(typeof(ILogger<GetAllArticlesLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetAllArticlesLogger(logger,
                    new GetAllArticlesValidator(
                        new GetAllArticlesRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
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
            [FromQuery] GetAllArticlesModelsHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllArticlesModelsLogger>)_services.GetService(typeof(ILogger<GetAllArticlesModelsLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetAllArticlesModelsLogger(logger,
                    new GetAllArticlesModelsValidator(
                        new GetAllArticlesModelsRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
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
            [FromQuery] GetAllArticlesBrandsHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllArticlesBrandsLogger>)_services.GetService(typeof(ILogger<GetAllArticlesBrandsLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetAllArticlesBrandsLogger(logger,
                    new GetAllArticlesBrandsValidator(
                        new GetAllArticlesBrandsRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
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
            [FromQuery] GetAllArticlesCategoriesHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllArticlesCategoriesLogger>)_services.GetService(typeof(ILogger<GetAllArticlesCategoriesLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetAllArticlesCategoriesLogger(logger,
                    new GetAllArticlesCategoriesValidator(
                        new GetAllArticlesCategoriesRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
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
            [FromQuery] GetArticleByIdHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetArticleByIdLogger>)_services.GetService(typeof(ILogger<GetArticleByIdLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetArticleByIdLogger(logger,
                    new GetArticleByIdValidator(
                        new GetArticleByIdRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
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
            [FromBody] CreateArticleHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<CreateArticleLogger>)_services.GetService(typeof(ILogger<CreateArticleLogger>));
            
            // Send request to pipeline
            var result = 
                await new CreateArticleLogger(logger,
                    new CreateArticleValidator(
                        new CreateArticleRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
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
            [FromBody] CreateCategoryHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<CreateCategoryLogger>)_services.GetService(typeof(ILogger<CreateCategoryLogger>));
            
            // Send request to pipeline
            var result = 
                await new CreateCategoryLogger(logger,
                    new CreateCategoryValidator(
                        new CreateCategoryRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
            return result switch
            {
                ICreateCategorySuccessResultContract => Ok(),
                ICreateCategoryErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}