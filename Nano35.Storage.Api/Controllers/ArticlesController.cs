using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CheckExistArticle;
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
            var result =
                await new LoggedUseCasePipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllArticlesRequestContract>)) as ILogger<IGetAllArticlesRequestContract>,
                        new GetAllArticlesUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllArticlesRequestContract()
                    {
                        InstanceId = query.InstanceId
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
    
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetArticleByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetArticleByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetArticleById(Guid id)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>(
                        _services.GetService(typeof(ILogger<IGetArticleByIdRequestContract>)) as ILogger<IGetArticleByIdRequestContract>,
                        new GetArticleByIdUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetArticleByIdRequestContract()
                    {
                        Id = id
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateArticleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateArticleErrorHttpResponse))] 
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleHttpBody body)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateArticleRequestContract, ICreateArticleResultContract>(
                        _services.GetService(typeof(ILogger<ICreateArticleRequestContract>)) as ILogger<ICreateArticleRequestContract>,
                        new CreateArticleUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new CreateArticleRequestContract()
                    {
                        NewId = body.NewId,
                        InstanceId = body.InstanceId,
                        Model = body.Model,
                        Brand = body.Brand,
                        CategoryId = body.CategoryId,
                        Info = body.Info,
                        Specs = body
                            .Specs
                            .Select(a =>
                                new SpecViewModel()
                                {
                                    Key = a.Key,
                                    Value = a.Value
                                }).ToList()
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
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
        
        [HttpGet]
        [Route("ExistArticle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsErrorHttpResponse))] 
        public async Task<IActionResult> CheckExistArticle(Guid id) 
        {
            var result =
                await new LoggedUseCasePipeNode<ICheckExistArticleRequestContract, ICheckExistArticleResultContract>(
                        _services.GetService(typeof(ILogger<ICheckExistArticleRequestContract>)) as ILogger<ICheckExistArticleRequestContract>,
                        new CheckExistArticleUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new CheckExistArticleRequestContract()
                    {
                        ArticleId = id
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}