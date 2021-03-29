using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
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
using Nano35.Storage.Api.Requests.GetAllPlacesOnStorage;
using Nano35.Storage.Api.Requests.GetAllSelleDetails;
using Nano35.Storage.Api.Requests.GetAllSells;
using Nano35.Storage.Api.Requests.GetAllWarehouseNames;
using Nano35.Storage.Api.Requests.GetAllWarehouseOfStorageItem;

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
            
            return await new ConvertedCreateComingOnHttpContext(
                        new LoggedCreateComingRequest(logger,
                            new ValidatedCreateComingRequest(
                                new CreateComingUseCase(bus)))).Ask(body);
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

            return await new ConvertedGetAllComingsOnHttpContext(
                        new LoggedGetAllComingsRequest(logger,
                            new ValidatedGetAllComingsRequest(
                                new GetAllComingsUseCase(bus)))).Ask(query);
        }
        
        [HttpGet]
        [Route("GetComingDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetComingDetailsByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetComingDetailsByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetComingDetails(
            [FromQuery] GetAllComingDetailsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllComingDetailsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllComingDetailsRequest>));

            return await new ConvertedGetAllComingDetailsOnHttpContext(
                new LoggedGetAllComingDetailsRequest(logger,
                    new ValidatedGetAllComingDetailsRequest(
                        new GetAllComingDetailsUseCase(bus)))).Ask(query);
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
            
            return await new ConvertedCreateMoveOnHttpContext(
                        new LoggedCreateMoveRequest(logger,
                            new ValidatedCreateMoveRequest(
                                new CreateMoveUseCase(bus)))).Ask(body);
        }

        [HttpGet]
        [Route("GetAllMoves")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMovesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMovesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoves(
            [FromQuery] GetAllMovesHttpQuery body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllMovesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllMovesRequest>));
            
            return await new ConvertedGetAllMovesOnHttpContext(
                new LoggedGetAllMovesRequest(logger,
                    new ValidatedGetAllMovesRequest(
                        new GetAllMovesUseCase(bus)))).Ask(body);
        }
        
        [HttpGet]
        [Route("GetAllMoveDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMoveDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMoveDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoveDetails(
            [FromQuery] GetAllMoveDetailsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllMoveDetailsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllMoveDetailsRequest>));

            return await new ConvertedGetAllMoveDetailsOnHttpContext(
                new LoggedGetAllMoveDetailsRequest(logger,
                    new ValidatedGetAllMoveDetailsRequest(
                        new GetAllMoveDetailsUseCase(bus)))).Ask(query);
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
            
            return await new ConvertedCreateSelleOnHttpContext(
                        new LoggedCreateSelleRequest(logger,
                            new ValidatedCreateSelleRequest(
                                new CreateSelleUseCase(bus)))).Ask(body);
        }

        [HttpGet]
        [Route("GetAllSells")]
        public async Task<IActionResult> GetAllSells(
            [FromQuery] GetAllSellsHttpQuery body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllSellsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllSellsRequest>));
            
            return await new ConvertedGetAllSellsOnHttpContext(
                new LoggedGetAllSellsRequest(logger,
                    new ValidatedGetAllSellsRequest(
                        new GetAllSellsUseCase(bus)))).Ask(body);
        }

        
        [HttpGet]
        [Route("GetAllSellDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSellDetails(
            [FromQuery] GetAllSellDetailsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllSelleDetailsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllSelleDetailsRequest>));

            return await new ConvertedGetAllSelleDetailsOnHttpContext(
                new LoggedGetAllSelleDetailsRequest(logger,
                    new ValidatedGetAllSelleDetailsRequest(
                        new GetAllSelleDetailsUseCase(bus)))).Ask(query);
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
                                new CreateCancellationUseCase(bus)))).Ask(body);
        }

        [HttpGet]
        [Route("GetAllCancellations")]
        public async Task<IActionResult> GetAllCancellations(
            [FromQuery] GetAllCancellationsHttpQuery body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllCancellationsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllCancellationsRequest>));
            
            return await new ConvertedGetAllCancellationsOnHttpContext(
                new LoggedGetAllCancellationsRequest(logger,
                    new ValidatedGetAllCancellationsRequest(
                        new GetAllCancellationsUseCase(bus)))).Ask(body);
        }

        
        [HttpGet]
        [Route("GetAllCancellationDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCancellationDetails(
            [FromQuery] GetAllCancellationDetailsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllCancellationDetailsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllCancellationDetailsRequest>));

            return await new ConvertedGetAllCancellationDetailsOnHttpContext(
                new LoggedGetAllCancellationDetailsRequest(logger,
                    new ValidatedGetAllCancellationDetailsRequest(
                        new GetAllCancellationDetailsUseCase(bus)))).Ask(query);
        }
        
        [HttpGet]
        [Route("GetAllPlacesOnStorage")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOnStorageSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOnStorageErrorResultContract))] 
        public async Task<IActionResult> GetAllPlacesOnStorage(
            [FromQuery] GetAllPlacesOnStorageHttpContext body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllPlacesOnStorageRequest>) _services.GetService(typeof(ILogger<LoggedGetAllPlacesOnStorageRequest>));

            return await new ConvertedGetAllPlacesOnStorageOnHttpContext(
                        new LoggedGetAllPlacesOnStorageRequest(logger,
                            new ValidatedGetAllPlacesOnStorageRequest(
                                new GetAllPlacesOnStorageUseCase(bus)))).Ask(body);
        }
        
        [HttpGet]
        [Route("GetAllWarehouseNames")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWarehouseNamesSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWarehouseNamesErrorResultContract))] 
        public async Task<IActionResult> GetAllWarehouseNames(
            [FromQuery] GetAllWarehouseNamesHttpQuery body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWarehouseNamesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllWarehouseNamesRequest>));

            return await new ConvertedGetAllWarehouseNamesOnHttpContext(
                new LoggedGetAllWarehouseNamesRequest(logger,
                    new ValidatedGetAllWarehouseNamesRequest(
                        new GetAllWarehouseNamesUseCase(bus)))).Ask(body);
        }
        
        [HttpGet]
        [Route("GetAllWarehouseOfStorageItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWarehouseOfStorageItemSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWarehouseOfStorageItemErrorResultContract))] 
        public async Task<IActionResult> GetAllWarehouseOfStorageItem(
            [FromQuery] GetAllWarehouseOfStorageItemHttpQuery body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWarehouseOfStorageItemRequest>) _services.GetService(typeof(ILogger<LoggedGetAllWarehouseOfStorageItemRequest>));

            return await new ConvertedGetAllWarehouseOfStorageItemOnHttpContext(
                new LoggedGetAllWarehouseOfStorageItemRequest(logger,
                    new ValidatedGetAllWarehouseOfStorageItemRequest(
                        new GetAllWarehouseOfStorageItemUseCase(bus)))).Ask(body);
        }
    }
}