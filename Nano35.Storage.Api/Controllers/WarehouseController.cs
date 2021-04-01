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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellsErrorHttpResponse))] 
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllCancellationsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllCancellationsErrorHttpResponse))] 
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
        [Route("GetAllPlacesOfStorageItemOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnUnitSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnUnitErrorResultContract))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnUnit(
            [FromQuery] GetAllPlacesOfStorageItemOnUnitHttpQuery body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest>) _services.GetService(typeof(ILogger<LoggedGetAllPlacesOfStorageItemOnUnitRequest>));

            return await new ConvertedGetAllPlacesOfStorageItemOnUnitOnHttpContext(
                new LoggedGetAllPlacesOfStorageItemOnUnitRequest(logger,
                    new ValidatedGetAllPlacesOfStorageItemOnUnitRequest(
                        new GetAllPlacesOfStorageItemOnUnitUseCase(bus)))).Ask(body);
        }
        
        [HttpGet]
        [Route("GetAllPlacesOfStorageItemOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnInstanceSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnInstanceErrorResultContract))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnInstance(
            [FromQuery] GetAllPlacesOfStorageItemOnInstanceHttpQuery body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest>) _services.GetService(typeof(ILogger<LoggedGetAllPlacesOfStorageItemOnInstanceRequest>));

            return await new ConvertedGetAllPlacesOfStorageItemOnInstanceOnHttpContext(
                new LoggedGetAllPlacesOfStorageItemOnInstanceRequest(logger,
                    new ValidatedGetAllPlacesOfStorageItemOnInstanceRequest(
                        new GetAllPlacesOfStorageItemOnInstanceUseCase(bus)))).Ask(body);
        }
        
        [HttpGet]
        [Route("GetAllStorageItemsOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnInstanceSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnInstanceErrorResultContract))] 
        public async Task<IActionResult> GetAllStorageItemsOnInstance(
            [FromQuery] GetAllStorageItemsOnInstanceHttpQuery body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllStorageItemsOnInstanceRequest>) _services.GetService(typeof(ILogger<LoggedGetAllStorageItemsOnInstanceRequest>));

            return await new ConvertedGetAllStorageItemsOnInstanceOnHttpContext(
                        new LoggedGetAllStorageItemsOnInstanceRequest(logger,
                            new ValidatedGetAllStorageItemsOnInstanceRequest(
                                new GetAllStorageItemsOnInstanceUseCase(bus)))).Ask(body);
        }
        
        [HttpGet]
        [Route("GetAllStorageItemsOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnUnitSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnUnitErrorResultContract))] 
        public async Task<IActionResult> GetAllStorageItemsOnUnit(
            [FromQuery] GetAllStorageItemsOnUnitHttpQuery body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllStorageItemsOnUnitRequest>) _services.GetService(typeof(ILogger<LoggedGetAllStorageItemsOnUnitRequest>));

            return await new ConvertedGetAllStorageItemsOnUnitOnHttpContext(
                        new LoggedGetAllStorageItemsOnUnitRequest(logger,
                            new ValidatedGetAllStorageItemsOnUnitRequest(
                                new GetAllStorageItemsOnUnitUseCase(bus)))).Ask(body);
        }
    }
}