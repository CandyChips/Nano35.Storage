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
using Nano35.Storage.Api.Requests.CreateCancellation;
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.CreateMove;
using Nano35.Storage.Api.Requests.CreateSelle;
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleModels;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetAllCancellationDetails;
using Nano35.Storage.Api.Requests.GetAllCancellations;
using Nano35.Storage.Api.Requests.GetAllComingDetails;
using Nano35.Storage.Api.Requests.GetAllMoveDetails;
using Nano35.Storage.Api.Requests.GetAllMoves;
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
    public class CancellationsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public CancellationsController(IServiceProvider services) { _services = services; }
    
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCancellationSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCancellationErrorHttpResponse))] 
        public async Task<IActionResult> CreateCancellation([FromBody] CreateCancellationHttpBody body) =>
            await new ConvertedCreateCancellationOnHttpContext(
                new LoggedPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(
                    _services.GetService(typeof(ILogger<ICreateCancellationRequestContract>)) as ILogger<ICreateCancellationRequestContract>,
                    new ValidatedPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(
                        _services.GetService(typeof(IValidator<ICreateCancellationRequestContract>)) as IValidator<ICreateCancellationRequestContract>,
                        new CreateCancellationUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllCancellationsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllCancellationsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCancellations([FromQuery] GetAllCancellationsHttpQuery body) =>
            await new CanonicalizedGetAllCancellationsRequest(
                new LoggedPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllCancellationsRequestContract>)) as ILogger<IGetAllCancellationsRequestContract>,
                    new ValidatedPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllCancellationsRequestContract>)) as IValidator<IGetAllCancellationsRequestContract>,
                        new GetAllCancellationsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        
        [HttpGet("{id}/details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllCancellationDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllCancellationDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCancellationDetails(Guid id) =>
            await new CanonicalizedGetAllCancellationDetailsRequest(
                    new LoggedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllCancellationDetailsRequestContract>)) as ILogger<IGetAllCancellationDetailsRequestContract>,
                        new ValidatedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(
                            _services.GetService(typeof(IValidator<IGetAllCancellationDetailsRequestContract>)) as IValidator<IGetAllCancellationDetailsRequestContract>,
                            new GetAllCancellationDetailsUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllCancellationDetailsHttpQuery() {CancellationId = id});
        
    }
}