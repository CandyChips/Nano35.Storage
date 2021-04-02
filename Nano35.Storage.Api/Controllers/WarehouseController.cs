using System;
using System.Threading.Tasks;
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

        [HttpPost]
        [Route("CreateComing")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateComingSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateComingErrorHttpResponse))] 
        public async Task<IActionResult> CreateComing(
            [FromBody] CreateComingHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<ICreateComingRequestContract>)_services.GetService(typeof(ILogger<ICreateComingRequestContract>));
            
            return await new CanonicalizedCreateComingRequest(
                        new LoggedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(logger,
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
            var logger = (ILogger<IGetAllComingsRequestContract>)_services.GetService(typeof(ILogger<IGetAllComingsRequestContract>));

            return await new CanonicalizedGetAllComingsRequest(
                        new LoggedPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(logger,
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
            var logger = (ILogger<IGetAllComingDetailsRequestContract>)_services.GetService(typeof(ILogger<IGetAllComingDetailsRequestContract>));

            return await new CanonicalizedGetAllComingDetailsRequest(
                new LoggedPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(logger,
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
            var logger = (ILogger<ICreateMoveRequestContract>)_services.GetService(typeof(ILogger<ICreateMoveRequestContract>));
            
            return await new CanonicalizedCreateMoveRequest(
                new LoggedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(logger,
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
            var logger = (ILogger<IGetAllMovesRequestContract>)_services.GetService(typeof(ILogger<IGetAllMovesRequestContract>));
            
            return await new CanonicalizedGetAllMovesRequest(
                new LoggedPipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract>(logger,
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
            var logger = (ILogger<IGetAllMoveDetailsRequestContract>)_services.GetService(typeof(ILogger<IGetAllMoveDetailsRequestContract>));

            return await new CanonicalizedGetAllMoveDetailsRequest(
                new LoggedPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(logger,
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
            var logger = (ILogger<ICreateSelleRequestContract>)_services.GetService(typeof(ILogger<ICreateSelleRequestContract>));
            
            return await new CanonicalizedCreateSelleRequest(
                new LoggedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(logger,
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
            var logger = (ILogger<IGetAllSellsRequestContract>)_services.GetService(typeof(ILogger<IGetAllSellsRequestContract>));
            
            return await new ConvertedGetAllSellsOnHttpContext(
                new LoggedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(logger,
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
            var logger = (ILogger<IGetAllSelleDetailsRequestContract>)_services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>));

            return await new CanonicalizedGetAllSelleDetailsRequest(
                new LoggedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(logger,
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
            var logger = (ILogger<ICreateCancellationRequestContract>)_services.GetService(typeof(ILogger<ICreateCancellationRequestContract>));
            
            return await new ConvertedCreateCancellationOnHttpContext(
                        new LoggedPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(logger,
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
            var logger = (ILogger<IGetAllCancellationsRequestContract>)_services.GetService(typeof(ILogger<IGetAllCancellationsRequestContract>));
            
            return await new CanonicalizedGetAllCancellationsRequest(
                new LoggedPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(logger,
                    new ValidatedGetAllCancellationsRequest(
                        new GetAllCancellationsUseCase(bus)))).Ask(body);
        }
        
        [HttpGet]
        [Route("GetAllCancellationDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllCancellationDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllCancellationDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCancellationDetails(
            [FromQuery] GetAllCancellationDetailsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllCancellationDetailsRequestContract>)_services.GetService(typeof(ILogger<IGetAllCancellationDetailsRequestContract>));

            return await new CanonicalizedGetAllCancellationDetailsRequest(
                new LoggedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(logger,
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
            var logger = (ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>) _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>));

            return await new CanonicalizedGetAllPlacesOfStorageItemOnUnitRequest(
                new LoggedPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract, IGetAllPlacesOfStorageItemOnUnitResultContract>(logger,
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
            var logger = (ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>) _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>));

            return await new CanonicalizedGetAllPlacesOfStorageItemOnInstanceRequest(
                new LoggedPipeNode<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>(logger,
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
            var logger = (ILogger<IGetAllStorageItemsOnInstanceContract>) _services.GetService(typeof(ILogger<IGetAllStorageItemsOnInstanceContract>));

            return await new CanonicalizedGetAllStorageItemsOnInstanceRequest(
                new LoggedPipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(logger,
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
            var logger = (ILogger<IGetAllStorageItemsOnUnitContract>) _services.GetService(typeof(ILogger<IGetAllStorageItemsOnUnitContract>));

            return await new CanonicalizedGetAllStorageItemsOnUnitRequest(
                new LoggedPipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(logger,
                    new ValidatedGetAllStorageItemsOnUnitRequest(
                        new GetAllStorageItemsOnUnitUseCase(bus)))).Ask(body);
        }
    }
}