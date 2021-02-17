﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.CreateArticle;
using Nano35.Storage.Api.Requests.CreateCategory;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleCategories;
using Nano35.Storage.Api.Requests.GetAllArticleModels;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetArticleById;
using CreateArticleHttpContext = Nano35.Storage.Api.HttpContext.CreateArticleHttpContext;
using CreateCategoryHttpContext = Nano35.Storage.Api.HttpContext.CreateCategoryHttpContext;
using GetAllArticlesBrandsHttpContext = Nano35.Storage.Api.HttpContext.GetAllArticlesBrandsHttpContext;
using GetAllArticlesCategoriesHttpContext = Nano35.Storage.Api.HttpContext.GetAllArticlesCategoriesHttpContext;
using GetAllArticlesHttpContext = Nano35.Storage.Api.HttpContext.GetAllArticlesHttpContext;
using GetAllArticlesModelsHttpContext = Nano35.Storage.Api.HttpContext.GetAllArticlesModelsHttpContext;
using GetArticleByIdHttpContext = Nano35.Storage.Api.HttpContext.GetArticleByIdHttpContext;

namespace Nano35.Storage.Api.Controllers
{
    /// ToDo Hey Maslyonok
    /// <summary>
    /// http://localhost:5104/articles/[action]
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController :
        ControllerBase
    {
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
        public async Task<IActionResult> GetAllArticles(
            [FromQuery] GetAllArticlesHttpContext query)
        {
            // ToDo Hey Maslyonok
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllArticlesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesRequest>));
            
            // ToDo Hey Maslyonok
            // Send request to pipeline
            var result = 
                await new LoggedGetAllArticlesRequest(logger,
                    new ValidatedGetAllArticlesRequest(
                        new GetAllArticlesRequest(bus)
                        )).Ask(query);
            
            // ToDo Hey Maslyonok
            // Check response of get all instances request
            // You can check result by result contracts
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
            var logger = (ILogger<LoggedGetAllArticlesModelsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesModelsRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllArticlesModelsRequest(logger,
                    new ValidatedGetAllArticlesModelsRequest(
                        new GetAllArticlesModelsRequest(bus)
                        )).Ask(query);
            
            // Check response of get all instances request
            // You can check result by result contracts
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
            var logger = (ILogger<LoggedGetAllArticlesBrandsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesBrandsRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllArticlesBrandsRequest(logger,
                    new ValidatedGetAllArticlesBrandsRequest(
                        new GetAllArticlesBrandsRequest(bus)
                        )).Ask(query);
            
            // Check response of get all instances request
            // You can check result by result contracts
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
            var logger = (ILogger<LoggedGetAllArticlesCategoriesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllArticlesCategoriesRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllArticlesCategoriesRequest(logger,
                    new ValidatedGetAllArticlesCategoriesRequest(
                        new GetAllArticlesCategoriesRequest(bus)
                        )).Ask(query);
            
            // Check response of get all instances request
            // You can check result by result contracts
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
            var logger = (ILogger<LoggedGetArticleByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetArticleByIdRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetArticleByIdRequest(logger,
                    new ValidatedGetArticleByIdRequest(
                        new GetArticleByIdRequest(bus)
                        )).Ask(query);
            
            // Check response of get all instances request
            // You can check result by result contracts
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
            var logger = (ILogger<LoggedCreateArticleRequest>)_services.GetService(typeof(ILogger<LoggedCreateArticleRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateArticleRequest(logger,
                    new ValidatedCreateArticleRequest(
                        new CreateArticleRequest(bus)
                        )).Ask(query);
            
            // Check response of get all instances request
            // You can check result by result contracts
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
            var logger = (ILogger<LoggedCreateCategoryRequest>)_services.GetService(typeof(ILogger<LoggedCreateCategoryRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateCategoryRequest(logger,
                    new ValidatedCreateCategoryRequest(
                        new CreateCategoryRequest(bus)
                        )).Ask(query);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateCategorySuccessResultContract => Ok(),
                ICreateCategoryErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}