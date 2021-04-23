using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.GetAllComingDetails;
using Nano35.Storage.Api.Requests.GetAllComings;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComingsController:
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public ComingsController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateComingSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateComingErrorHttpResponse))] 
        public async Task<IActionResult> CreateComing(
            [FromBody] CreateComingHttpBody body) =>
            await new CanonicalizedCreateComingRequest(
                    new LoggedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(
                        _services.GetService(typeof(ILogger<ICreateComingRequestContract>)) as ILogger<ICreateComingRequestContract>,
                        new CreateComingUseCase(_services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);
        
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllComingsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllComingsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllComings(
            [FromHeader] GetAllComingsHttpQuery query) =>
            await new CanonicalizedGetAllComingsRequest(
                    new LoggedPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllComingsRequestContract>)) as ILogger<IGetAllComingsRequestContract>,
                        new GetAllComingsUseCase(_services.GetService(typeof(IBus)) as IBus)))
                .Ask(query);
        
        [HttpGet]
        [Route("{id}/Details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetComingDetailsByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetComingDetailsByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetComingDetails(Guid id) => 
            await new CanonicalizedGetAllComingDetailsRequest(
                    new LoggedPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllComingDetailsRequestContract>)) as ILogger<IGetAllComingDetailsRequestContract>,
                        new GetAllComingDetailsUseCase(_services.GetService(typeof(IBus)) as IBus)))
                .Ask(id);
    }
}