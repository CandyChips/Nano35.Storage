using System;
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
using Nano35.Storage.Api.Requests.CreateArticle;
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.CreateSelle;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleModels;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetAllComingDetails;
using Nano35.Storage.Api.Requests.GetAllSelleDetails;
using Nano35.Storage.Api.Requests.GetAllSells;
using Nano35.Storage.Api.Requests.GetArticleById;
using Nano35.Storage.Api.Requests.UpdateArticleBrand;
using Nano35.Storage.Api.Requests.UpdateArticleCategory;
using Nano35.Storage.Api.Requests.UpdateArticleInfo;
using Nano35.Storage.Api.Requests.UpdateArticleModel;
using CreateSelleDetailViewModel = Nano35.Contracts.Storage.Models.CreateSelleDetailViewModel;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public SellsController(IServiceProvider services) { _services = services; }
    
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateSelleErrorHttpResponse))] 
        public async Task<IActionResult> CreateSelle([FromBody] CreateSelleHttpBody body)
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                        _services.GetService(typeof(ILogger<ICreateSelleRequestContract>)) as ILogger<ICreateSelleRequestContract>,
                        new CreateSelleUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new CreateSelleRequestContract()
                    {
                        NewId = body.NewId,
                        InstanceId = body.InstanceId,
                        UnitId = body.UnitId,
                        Number = body.Number,
                        Details = body
                            .Details
                            .Select(a=> 
                                new CreateSelleDetailViewModel()
                                {
                                    NewId = a.NewId,
                                    Count = a.Count,
                                    PlaceOnStorage = a.PlaceOnStorage,
                                    StorageItemId = a.StorageItemId,
                                    Price = a.Price
                                }).ToList()
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSells([FromQuery] GetAllSellsHttpQuery body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllSellsRequestContract>)) as ILogger<IGetAllSellsRequestContract>,
                        new GetAllSellsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllSellsRequestContract()
                    {
                        InstanceId = body.InstanceId,
                        UnitId = body.UnitId,
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        
        [HttpGet("{id}/details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSellDetails(Guid id) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>)) as ILogger<IGetAllSelleDetailsRequestContract>,
                        new GetAllSelleDetailsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllSelleDetailsRequestContract()
                    {
                        SelleId = id
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
    }
}