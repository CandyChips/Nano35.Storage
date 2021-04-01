﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    /// http://localhost:5003/articles/[action]
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
        public ArticlesController(IServiceProvider services) { _services = services; }
    
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
            
            // ToDo Hey Maslyonok
            // Building pipeline
            return await new ConvertedGetAllArticlesOnHttpContext(
                        new LoggedGetAllArticlesRequest(logger,
                            new ValidatedGetAllArticlesRequest(
                                new GetAllArticlesUseCase(bus)))).Ask(query);
            
        }
         
        [HttpGet]
        [Route("GetAllArticleModels")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleModelsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleModelsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleModels(
            [FromQuery] GetAllArticleModelsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllArticlesModelsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesModelsRequest>));

            return await new ConvertedGetAllArticleModelsOnHttpContext(
                        new LoggedGetAllArticlesModelsRequest(logger,
                            new ValidatedGetAllArticlesModelsRequest(
                                new GetAllArticlesModelsUseCase(bus)))).Ask(query);
        }
    
        [HttpGet]
        [Route("GetAllArticleBrands")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleBrandsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleBrandsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleBrands(
            [FromQuery] GetAllArticlesBrandsHttpQuery query)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllArticlesBrandsRequest>) _services.GetService(typeof(ILogger<LoggedGetAllArticlesBrandsRequest>));

            return await new ConvertedGetAllArticleBrandsOnHttpContext(
                        new LoggedGetAllArticlesBrandsRequest(logger,
                            new ValidatedGetAllArticlesBrandsRequest(
                                new GetAllArticleBrandsUseCase(bus)))).Ask(query);
        }
    
        [HttpGet]
        [Route("GetArticleById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetArticleByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetArticleByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetArticleById(
            [FromQuery] GetArticleByIdHttpQuery query)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetArticleByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetArticleByIdRequest>));

            return await new ConvertedGetArticleByIdOnHttpContext(
                        new LoggedGetArticleByIdRequest(logger,
                            new ValidatedGetArticleByIdRequest(
                                new GetArticleByIdUseCase(bus)))).Ask(query);
            
        }
        
        /// <summary>
        /// Creates a Article.
        /// </summary>
        /// <remarks>
        /// Note that the NewId, InstanceId and CategoryId is a GUID and not an integer.
        ///  
        ///     POST /CreateArticle
        ///     {
        ///        "NewId": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "InstanceId": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "Model": "Cool model",
        ///        "Brand": "Super brand",
        ///        "CategoryId": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "Info": "Awesome thing",
        ///        "Specs":
        ///        {
        ///            "Key": "Color",
        ///            "Value": "Black",
        ///        }
        ///     }
        /// 
        /// </remarks>
        /// <param name="body"></param>
        /// <returns>New Created Article</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
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

            return await new ConvertedCreateArticleOnHttpContext(
                        new LoggedCreateArticleRequest(logger,
                            new ValidatedCreateArticleRequest(
                                new CreateArticleUseCase(bus)))).Ask(body);
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

            return await new ConvertedUpdateArticleBrandOnHttpContext(
                        new LoggedUpdateArticleBrandRequest(logger,  
                            new ValidatedUpdateArticleBrandRequest(
                                new UpdateArticleBrandUseCase(bus)))).Ask(body);
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

            return await new ConvertedUpdateArticleCategoryOnHttpContext(
                        new LoggedUpdateArticleCategoryRequest(logger,  
                            new ValidatedUpdateArticleCategoryRequest(
                                new UpdateArticleCategoryUseCase(bus)))).Ask(body);
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

            return await new ConvertedUpdateArticleInfoOnHttpContext(
                        new LoggedUpdateArticleInfoRequest(logger,  
                            new ValidatedUpdateArticleInfoRequest(
                                new UpdateArticleInfoUseCase(bus)))).Ask(body);
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

            return await new ConvertedUpdateArticleModelOnHttpContext(
                        new LoggedUpdateArticleModelRequest(logger,  
                            new ValidatedUpdateArticleModelRequest(
                                new UpdateArticleModelUseCase(bus)))).Ask(body);
        }
    }
}