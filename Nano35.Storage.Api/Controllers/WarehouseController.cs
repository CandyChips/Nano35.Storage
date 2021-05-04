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
using Nano35.Storage.Api.Requests.CreateCancellation;
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.CreateMove;
using Nano35.Storage.Api.Requests.CreateSelle;
using Nano35.Storage.Api.Requests.GetAllCancellationDetails;
using Nano35.Storage.Api.Requests.GetAllCancellations;
using Nano35.Storage.Api.Requests.GetAllComingDetails;
using Nano35.Storage.Api.Requests.GetAllComings;
using Nano35.Storage.Api.Requests.GetAllMoveDetails;
using Nano35.Storage.Api.Requests.GetAllMoves;
using Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnInstance;
using Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnUnit;
using Nano35.Storage.Api.Requests.GetAllSelleDetails;
using Nano35.Storage.Api.Requests.GetAllSells;
using Nano35.Storage.Api.Requests.GetAllStorageItemsOnInstance;
using Nano35.Storage.Api.Requests.GetAllStorageItemsOnUnit;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public WarehouseController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Route("PlacesOfStorageItemOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnUnitSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnUnitErrorHttpResponse))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnUnit([FromQuery] GetAllPlacesOfStorageItemOnUnitHttpQuery body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract, IGetAllPlacesOfStorageItemOnUnitResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>)) as ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>,
                        new GetAllPlacesOfStorageItemOnUnitUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllPlacesOfStorageItemOnUnitRequestContract()
                    {
                        StorageItemId = body.StorageItemId,
                        UnitContainsStorageItemId = body.UnitContainsStorageItemId
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("PlacesOfStorageItemOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnInstanceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnInstanceErrorHttpResponse))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnInstance([FromQuery] GetAllPlacesOfStorageItemOnInstanceHttpQuery body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>)) as ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>,
                        new GetAllPlacesOfStorageItemOnInstanceUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllPlacesOfStorageItemOnInstanceContract(){StorageItemId = body.StorageItemId});
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("StorageItemsOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnInstanceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnInstanceErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItemsOnInstance([FromQuery] GetAllStorageItemsOnInstanceHttpQuery body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemsOnInstanceContract>)) as ILogger<IGetAllStorageItemsOnInstanceContract>,
                        new GetAllStorageItemsOnInstanceUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllStorageItemsOnInstanceContract(){InstanceId = body.InstanceId});
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("StorageItemsOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnUnitSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnUnitErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItemsOnUnit([FromQuery] GetAllStorageItemsOnUnitHttpQuery body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemsOnUnitContract>)) as ILogger<IGetAllStorageItemsOnUnitContract>,
                        new GetAllStorageItemsOnUnitUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllStorageItemsOnUnitContract(){UnitId = body.UnitId});
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}