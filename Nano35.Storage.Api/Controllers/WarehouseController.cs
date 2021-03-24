using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.HttpContext.instance;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests.CreateCancellation;
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.CreateMove;
using Nano35.Storage.Api.Requests.CreateSelle;
using Nano35.Storage.Api.Requests.GetAllComings;
using Nano35.Storage.Api.Requests.GetAllPlacesOnStorage;
using Nano35.Storage.Api.Requests.GetComingDetailsById;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public WarehouseController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("CreateComing")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateComingSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateComingErrorHttpResponse))] 
        public async Task<IActionResult> CreateComing(
            [FromBody] CreateComingHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateComingRequest>)_services.GetService(typeof(ILogger<LoggedCreateComingRequest>));
            
            return await
                new ConvertedCreateComingOnHttpContext(
                new LoggedCreateComingRequest(logger,
                    new ValidatedCreateComingRequest(
                        new CreateComingRequest(bus)))).Ask(body);
            
        }
        
        [HttpGet]
        [Route("GetComingDetailsById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetComingDetailsByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetComingDetailsByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetComingDetailsById(
            [FromQuery] GetComingDetailsByIdHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetComingDetailsByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetComingDetailsByIdRequest>));

            var request = new GetComingDetailsByIdRequestContract()
            {
                Id = query.Id
            };
            
            var result = await new LoggedGetComingDetailsByIdRequest(logger,
                        new ValidatedGetComingDetailsByIdRequest(
                            new GetComingDetailsByIdRequest(bus))).Ask(request);
            
            return result switch
            {
                IGetComingDetailsByIdSuccessResultContract success => Ok(success),
                IGetComingDetailsByIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpGet]
        [Route("GetAllComings")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllComingsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllComingsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllComings(
            [FromHeader] GetAllComingsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllComingsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllComingsRequest>));
            
            var request = new GetAllComingsRequestContract()
            {
                InstanceId = query.InstanceId,
                StorageItemId = query.StorageItemId,
                UnitId = query.UnitId,
            };
            
            var result = await new LoggedGetAllComingsRequest(logger,
                            new ValidatedGetAllComingsRequest(
                                new GetAllComingsRequest(bus))).Ask(request);
            
            return result switch
            {
                IGetAllComingsSuccessResultContract success => Ok(success),
                IGetAllComingsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPost]
        [Route("CreateMove")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateMoveSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateMoveErrorHttpResponse))] 
        public async Task<IActionResult> CreateMove(
            [FromBody] CreateMoveHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateMoveRequest>)_services.GetService(typeof(ILogger<LoggedCreateMoveRequest>));
            
            return await 
                new ConvertedCreateMoveOnHttpContext(
                new LoggedCreateMoveRequest(logger,
                    new ValidatedCreateMoveRequest(
                        new CreateMoveRequest(bus)))).Ask(body);
        }

        [HttpGet]
        [Route("GetAllMoves")]
        public async Task<IActionResult> GetAllMoves()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetAllMoveDetailsById")]
        public async Task<IActionResult> GetAllMoveDetailsById()
        {
            return Ok();
        }
        
        [HttpPost]
        [Route("CreateSelle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateSelleErrorHttpResponse))] 
        public async Task<IActionResult> CreateSelle(
            [FromBody] CreateSelleHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateSelleRequest>)_services.GetService(typeof(ILogger<LoggedCreateSelleRequest>));
            
            return await 
                new ConvertedCreateSelleOnHttpContext(
                new LoggedCreateSelleRequest(logger,
                    new ValidatedCreateSelleRequest(
                        new CreateSelleRequest(bus)))).Ask(body);
        }

        [HttpGet]
        [Route("GetAllSells")]
        public async Task<IActionResult> GetAllSells()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetAllSelleDetailsById")]
        public async Task<IActionResult> GetAllSelleDetailsById()
        {
            return Ok();
        }
        
        [HttpGet]
        [Route("GetAllPlacesOnStorage")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOnStorageSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOnStorageErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItems(
            [FromQuery] GetAllPlacesOnStorageHttpContext body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllPlacesOnStorageRequest>) _services.GetService(typeof(ILogger<LoggedGetAllPlacesOnStorageRequest>));

            return await new ConvertedGetAllPlacesOnStorageOnHttpContext(
                    new LoggedGetAllPlacesOnStorageRequest(logger,
                        new ValidatedGetAllPlacesOnStorageRequest(
                            new GetAllPlacesOnStorageRequest(bus)))).Ask(body);
            
        }
        
        [HttpPost]
        [Route("CreateCancellation")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCancellationSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCancellationErrorHttpResponse))] 
        public async Task<IActionResult> CreateCancellation(
            [FromBody] CreateCancellationHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateCancellationRequest>)_services.GetService(typeof(ILogger<LoggedCreateCancellationRequest>));

            return await new ConvertedCreateCancellationOnHttpContext(
                        new LoggedCreateCancellationRequest(logger,
                            new ValidatedCreateCancellationRequest(
                                new CreateCancellationRequest(bus)))).Ask(body);
        }
    }
}