using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateMove;
using Nano35.Storage.Api.Requests.GetAllComingDetails;
using Nano35.Storage.Api.Requests.GetAllMoveDetails;
using Nano35.Storage.Api.Requests.GetAllMoves;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovesController:
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public MovesController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateMoveSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateMoveErrorHttpResponse))] 
        public async Task<IActionResult> CreateMove(
            [FromBody] CreateMoveHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(
                        _services.GetService(typeof(ILogger<ICreateMoveRequestContract>)) as ILogger<ICreateMoveRequestContract>,
                        new CreateMoveUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new CreateMoveRequestContract()
                    {
                        NewId = body.NewId,
                        InstanceId = body.InstanceId,
                        FromUnitId = body.FromUnitId,
                        ToUnitId = body.ToUnitId,
                        Details = body
                            .Details
                            .Select(a=> 
                                new Contracts.Storage.Models.CreateMoveDetailViewModel()
                        {
                            NewId = a.NewId,
                            Count = a.Count,
                            FromPlaceOnStorage = a.FromPlaceOnStorage,
                            ToPlaceOnStorage = a.ToPlaceOnStorage,
                            StorageItemId = a.StorageItemId
                        }).ToList()
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMovesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMovesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoves(
            [FromQuery] GetAllMovesHttpQuery body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllMovesRequestContract>)) as ILogger<IGetAllMovesRequestContract>,
                        new GetAllMovesUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllMovesRequestContract()
                    {
                        InstanceId = body.InstanceId,
                        UnitId = body.UnitId
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("{id}/Details")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMoveDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMoveDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoveDetails(Guid id) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllMoveDetailsRequestContract>)) as ILogger<IGetAllMoveDetailsRequestContract>,
                        new GetAllMoveDetailsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllMoveDetailsRequestContract() {MoveId = id});
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}