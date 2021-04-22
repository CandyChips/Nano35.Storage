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
using Nano35.Storage.Api.Requests.CreateMove;
using Nano35.Storage.Api.Requests.GetAllMoveDetails;
using Nano35.Storage.Api.Requests.GetAllMoves;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovesController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public MovesController(IServiceProvider services) { _services = services; }
    
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateMoveSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateMoveErrorHttpResponse))] 
        public async Task<IActionResult> CreateMove([FromBody] CreateMoveHttpBody body) =>
            await new CanonicalizedCreateMoveRequest(
                new LoggedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(
                    _services.GetService(typeof(ILogger<ICreateMoveRequestContract>)) as ILogger<ICreateMoveRequestContract>,
                    new ValidatedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(
                        _services.GetService(typeof(IValidator<ICreateMoveRequestContract>)) as IValidator<ICreateMoveRequestContract>,
                        new CreateMoveUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMovesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMovesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoves([FromQuery] GetAllMovesHttpQuery body) =>
            await new CanonicalizedGetAllMovesRequest(
                new LoggedPipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllMovesRequestContract>)) as ILogger<IGetAllMovesRequestContract>,
                    new ValidatedPipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllMovesRequestContract>)) as IValidator<IGetAllMovesRequestContract>,
                        new GetAllMovesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        
        [HttpGet("{id}/Details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMoveDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMoveDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoveDetails(Guid id) =>
            await new CanonicalizedGetAllMoveDetailsRequest(
                new LoggedPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllMoveDetailsRequestContract>)) as ILogger<IGetAllMoveDetailsRequestContract>,
                    new ValidatedPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllMoveDetailsRequestContract>)) as IValidator<IGetAllMoveDetailsRequestContract>,
                        new GetAllMoveDetailsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllMoveDetailsHttpQuery() {MoveId = id});
        
    }
}