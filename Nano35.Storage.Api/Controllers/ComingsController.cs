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
using Nano35.Storage.Api.Requests.GetAllArticleBrands;
using Nano35.Storage.Api.Requests.GetAllArticleModels;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetAllComingDetails;
using Nano35.Storage.Api.Requests.GetAllComings;
using Nano35.Storage.Api.Requests.GetArticleById;
using Nano35.Storage.Api.Requests.UpdateArticleBrand;
using Nano35.Storage.Api.Requests.UpdateArticleCategory;
using Nano35.Storage.Api.Requests.UpdateArticleInfo;
using Nano35.Storage.Api.Requests.UpdateArticleModel;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComingsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public ComingsController(IServiceProvider services) { _services = services; }
    
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllComingsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllComingsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllComings([FromQuery] GetAllComingsHttpQuery query) =>
            await new CanonicalizedGetAllComingsRequest(
                new LoggedPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllComingsRequestContract>)) as ILogger<IGetAllComingsRequestContract>,
                    new ValidatedPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllComingsRequestContract>)) as IValidator<IGetAllComingsRequestContract>,
                        new GetAllComingsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);

        [HttpGet("{id}/Details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetComingDetailsByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetComingDetailsByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetComingDetails(Guid id)
        {
            return await new CanonicalizedGetAllComingDetailsRequest(
                new LoggedPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllComingDetailsRequestContract>)) as ILogger<IGetAllComingDetailsRequestContract>,
                    new ValidatedPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllComingDetailsRequestContract>)) as IValidator<IGetAllComingDetailsRequestContract>,
                        new GetAllComingDetailsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllComingDetailsHttpQuery() { ComingId = id });
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateComingSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateComingErrorHttpResponse))] 
        public async Task<IActionResult> CreateComing([FromBody] CreateComingHttpBody body)
        {
            return await new CanonicalizedCreateComingRequest(
                new LoggedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(
                    _services.GetService(typeof(ILogger<ICreateComingRequestContract>)) as ILogger<ICreateComingRequestContract>,
                    new ValidatedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(
                        _services.GetService(typeof(IValidator<ICreateComingRequestContract>)) as IValidator<ICreateComingRequestContract>,
                        new CreateComingUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }
    }
}