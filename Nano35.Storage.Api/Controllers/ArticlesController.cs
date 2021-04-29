﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
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
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public ArticlesController(IServiceProvider services) { _services = services; }
    
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticlesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticlesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticles([FromQuery] GetAllArticlesHttpQuery query)
        {
            return await new CanonicalizedGetAllArticlesRequest(
                new LoggedPipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllArticlesRequestContract>)) as ILogger<IGetAllArticlesRequestContract>,
                    new GetAllArticlesUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(query);
            
        }
         
        [HttpGet]
        [Route("Models")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleModelsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleModelsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleModels([FromQuery] GetAllArticleModelsHttpQuery query)
        {
            return await new CanonicalizedGetAllArticleModelsRequest(
                new LoggedPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllArticlesModelsRequestContract>)) as ILogger<IGetAllArticlesModelsRequestContract>,
                    new GetAllArticlesModelsUseCase(
                        _services.GetService(typeof(IBus)) as IBus))).Ask(query);
        }
    
        [HttpGet]
        [Route("Brands")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllArticleBrandsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllArticleBrandsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllArticleBrands([FromQuery] GetAllArticlesBrandsHttpQuery query)
        {
            return await new CanonicalizedGetAllArticleBrandsRequest(
                new LoggedPipeNode<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllArticlesBrandsRequestContract>)) as ILogger<IGetAllArticlesBrandsRequestContract>,
                    new GetAllArticleBrandsUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(query);
        }
    
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetArticleByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetArticleByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            return await new CanonicalizedGetArticleByIdRequest(
                new LoggedPipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetArticleByIdRequestContract>)) as ILogger<IGetArticleByIdRequestContract>,
                    new GetArticleByIdUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(new GetArticleByIdHttpQuery() {Id = id});
        }
        
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateArticleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateArticleErrorHttpResponse))] 
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleHttpBody body)
        {
            return await new CanonicalizedCreateArticleRequest(
                new LoggedPipeNode<ICreateArticleRequestContract, ICreateArticleResultContract>(
                    _services.GetService(typeof(ILogger<ICreateArticleRequestContract>)) as ILogger<ICreateArticleRequestContract>,
                    new CreateArticleUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
        }
        
        [HttpPatch]
        [Route("Category")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleCategorySuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleCategoryErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleCategory([FromBody] UpdateArticleCategoryHttpBody body)
        {
            return await new CanonicalizedUpdateArticleCategoryRequest(
                new LoggedPipeNode<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateArticleCategoryRequestContract>)) as ILogger<IUpdateArticleCategoryRequestContract>,  
                    new UpdateArticleCategoryUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
        }
        
        [HttpPatch]
        [Route("Info")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleInfoSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleInfoErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleInfo([FromBody] UpdateArticleInfoHttpBody body)
        {
            return await new CanonicalizedUpdateArticleInfoRequest(
                new LoggedPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateArticleInfoRequestContract>)) as ILogger<IUpdateArticleInfoRequestContract>,  
                    new UpdateArticleInfoUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
        }
        
        [HttpPatch]
        [Route("Model")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateArticleModelSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateArticleModelErrorHttpResponse))] 
        public async Task<IActionResult> UpdateArticleModel([FromBody] UpdateArticleModelHttpBody body)
        {
            return await new CanonicalizedUpdateArticleModelRequest(
                new LoggedPipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateArticleModelRequestContract>)) as ILogger<IUpdateArticleModelRequestContract>,  
                    new UpdateArticleModelUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
        }
    }
}