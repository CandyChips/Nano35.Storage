using System;
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

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellesController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public SellesController(IServiceProvider services) { _services = services; }
    
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateSelleErrorHttpResponse))] 
        public async Task<IActionResult> CreateSelle([FromBody] CreateSelleHttpBody body) =>
            await new CanonicalizedCreateSelleRequest(
                new LoggedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                    _services.GetService(typeof(ILogger<ICreateSelleRequestContract>)) as ILogger<ICreateSelleRequestContract>,
                    new ValidatedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                        _services.GetService(typeof(IValidator<ICreateSelleRequestContract>)) as IValidator<ICreateSelleRequestContract>,
                        new CreateSelleUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSells([FromQuery] GetAllSellsHttpQuery body) =>
            await new ConvertedGetAllSellsOnHttpContext(
                new LoggedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllSellsRequestContract>)) as ILogger<IGetAllSellsRequestContract>,
                    new ValidatedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllSellsRequestContract>)) as IValidator<IGetAllSellsRequestContract>,
                        new GetAllSellsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);

        
        [HttpGet("{id}/details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSellDetails(Guid id) =>
            await new CanonicalizedGetAllSelleDetailsRequest(
                new LoggedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>)) as ILogger<IGetAllSelleDetailsRequestContract>,
                    new ValidatedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllSelleDetailsRequestContract>)) as IValidator<IGetAllSelleDetailsRequestContract>,
                        new GetAllSelleDetailsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllSellDetailsHttpQuery() {SelleId = id});
        
    }
}