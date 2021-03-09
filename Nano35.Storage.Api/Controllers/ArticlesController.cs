using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests.CreateArticle;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleModels;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetArticleById;
using Nano35.Storage.Api.Requests.UpdateArticleBrand;
using Nano35.Storage.Api.Requests.UpdateArticleCategory;
using Nano35.Storage.Api.Requests.UpdateArticleInfo;
using Nano35.Storage.Api.Requests.UpdateArticleModel;

namespace Nano35.Storage.Api.Controllers
{
    /// ToDo Hey Maslyonok
    /// <summary>
    /// http://localhost:6003/articles/[action]
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController :
        ControllerBase
    {
        
        /// ToDo Hey Maslyonok
        /// <summary>
        /// _services provide a DI services to request
        /// </summary>
        private readonly IServiceProvider _services;
        
        /// ToDo Hey Maslyonok
        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public ArticlesController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        /// ToDo Hey Maslyonok
        /// <summary>
        /// GET -> http://localhost:5104/articles/GetAllArticles
        /// success -> Articles[]
        /// error -> string
        /// ---$---$---
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '''(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()''';
        /// 3. Response pattern match of pipeline response;
        /// </summary>
        [HttpGet]
        [Route("GetAllArticles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticlesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticlesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticles(
            [FromQuery] GetAllArticlesHttpQuery query)
        {
            // ToDo Hey Maslyonok
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllArticlesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesRequest>));

            var request = new GetAllArticlesRequestContract()
            {
                InstanceId = query.InstanceId
            };
            
            // ToDo Hey Maslyonok
            // Send request to pipeline
            var result = 
                await new LoggedGetAllArticlesRequest(logger,
                    new ValidatedGetAllArticlesRequest(
                        new GetAllArticlesRequest(bus)))
                    .Ask(request);
            
            // ToDo Hey Maslyonok
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                IGetAllArticlesSuccessResultContract success => Ok(success),
                IGetAllArticlesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
         
        [HttpGet]
        [Route("GetAllArticleModels")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleModelsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleModelsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleModels(
            [FromQuery] GetAllArticleModelsHttpQuery query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllArticlesModelsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesModelsRequest>));

            var request = new GetAllArticlesModelsRequestContract()
            {
                CategoryId = query.CategoryId,
                InstanceId = query.InstanceId
            };
            
            var result = 
                await new LoggedGetAllArticlesModelsRequest(logger,
                    new ValidatedGetAllArticlesModelsRequest(
                        new GetAllArticlesModelsRequest(bus))).Ask(request);
            
            return result switch
            {
                IGetAllArticlesModelsSuccessResultContract success => Ok(success),
                IGetAllArticlesModelsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllArticleBrands")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleBrandsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleBrandsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleBrands(
            [FromQuery] GetAllArticlesBrandsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllArticlesBrandsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesBrandsRequest>));

            var request = new GetAllArticlesBrandsRequestContract()
            {
                CategoryId = query.CategoryId,
                InstanceId = query.InstanceId
            };
            
            var result = 
                await new LoggedGetAllArticlesBrandsRequest(logger,
                    new ValidatedGetAllArticlesBrandsRequest(
                        new GetAllArticlesBrandsRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetAllArticlesBrandsSuccessResultContract success => Ok(success),
                IGetAllArticlesBrandsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetArticleById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetArticleByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetArticleByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetArticleById(
            [FromQuery] GetArticleByIdHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetArticleByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetArticleByIdRequest>));

            var request = new GetArticleByIdRequestContract()
            {
                Id = query.Id
            };
            
            var result = 
                await new LoggedGetArticleByIdRequest(logger,
                    new ValidatedGetArticleByIdRequest(
                        new GetArticleByIdRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetArticleByIdSuccessResultContract success => Ok(success.Data),
                IGetArticleByIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateArticle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateArticleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateArticleErrorHttpResponse))] 
        public async Task<IActionResult> CreateArticle(
            [FromBody] CreateArticleHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateArticleRequest>)_services.GetService(typeof(ILogger<LoggedCreateArticleRequest>));

            var request = new CreateArticleRequestContract()
            {
                Brand = body.Brand,
                CategoryId = body.CategoryId,
                Info = body.Info,
                InstanceId = body.InstanceId,
                Model = body.Model,
                NewId = body.NewId,
                Specs = body.Specs
            };
            
            var result = 
                await new LoggedCreateArticleRequest(logger,
                    new ValidatedCreateArticleRequest(
                        new CreateArticleRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                ICreateArticleSuccessResultContract => Ok(),
                ICreateArticleErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateArticleBrand")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleBrandSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleBrandErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleBrand(
            [FromBody] UpdateArticleBrandHttpBody body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateArticleBrandRequest>) _services.GetService(typeof(ILogger<LoggedUpdateArticleBrandRequest>));

            var request = new UpdateArticleBrandRequestContract()
            {
                Brand = body.Brand,
                Id = body.Id
            };
            
            var result =
                await new LoggedUpdateArticleBrandRequest(logger,  
                    new ValidatedUpdateArticleBrandRequest(
                        new UpdateArticleBrandRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateArticleBrandSuccessResultContract => Ok(),
                IUpdateArticleBrandErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateArticleCategory")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleCategorySuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleCategoryErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleCategory(
            [FromBody] UpdateArticleCategoryHttpBody body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateArticleCategoryRequest>) _services.GetService(typeof(ILogger<LoggedUpdateArticleCategoryRequest>));

            var request = new UpdateArticleCategoryRequestContract()
            {
                Id = body.Id,
                CategoryId = body.CategoryId
            };
            
            var result =
                await new LoggedUpdateArticleCategoryRequest(logger,  
                    new ValidatedUpdateArticleCategoryRequest(
                        new UpdateArticleCategoryRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateArticleCategorySuccessResultContract => Ok(),
                IUpdateArticleCategoryErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateArticleInfo")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleInfoSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleInfoErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleInfo(
            [FromBody] UpdateArticleInfoHttpBody body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateArticleInfoRequest>) _services.GetService(typeof(ILogger<LoggedUpdateArticleInfoRequest>));

            var request = new UpdateArticleInfoRequestContract()
            {
                Id = body.Id,
                Info = body.Info
            };
            
            var result =
                await new LoggedUpdateArticleInfoRequest(logger,  
                    new ValidatedUpdateArticleInfoRequest(
                        new UpdateArticleInfoRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateArticleInfoSuccessResultContract => Ok(),
                IUpdateArticleInfoErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateArticleModel")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleModelSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleModelErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleModel(
            [FromBody] UpdateArticleModelHttpBody body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateArticleModelRequest>) _services.GetService(typeof(ILogger<LoggedUpdateArticleModelRequest>));

            var request = new UpdateArticleModelRequestContract()
            {
                Id = body.Id,
                Model = body.Model
            };
            
            var result =
                await new LoggedUpdateArticleModelRequest(logger,  
                        new ValidatedUpdateArticleModelRequest(
                            new UpdateArticleModelRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateArticleModelSuccessResultContract => Ok(),
                IUpdateArticleModelErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpDelete]
        [Route("DeleteArticle")]
        public async Task<IActionResult> DeleteArticle()
        {
            return Ok();
        }
    }
}