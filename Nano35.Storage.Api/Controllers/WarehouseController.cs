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
        
        [HttpPost]
        [Route("CreateMove")]
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
        [Route("GetAllMoves")]
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
        
        [HttpGet]
        [Route("GetAllMoveDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMoveDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMoveDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoveDetails([FromQuery] GetAllMoveDetailsHttpQuery query) =>
            await new CanonicalizedGetAllMoveDetailsRequest(
                new LoggedPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllMoveDetailsRequestContract>)) as ILogger<IGetAllMoveDetailsRequestContract>,
                    new ValidatedPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllMoveDetailsRequestContract>)) as IValidator<IGetAllMoveDetailsRequestContract>,
                        new GetAllMoveDetailsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        
        [HttpPost]
        [Route("CreateSelle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateSelleErrorHttpResponse))] 
        public async Task<IActionResult> CreateSelle([FromBody] CreateSelleHttpBody body) =>
            await new CanonicalizedCreateSelleRequest(
                new LoggedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                    _services.GetService(typeof(ILogger<ICreateSelleRequestContract>)) as ILogger<ICreateSelleRequestContract>,
                    new ValidatedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                        _services.GetService(typeof(IValidator<ICreateSelleRequestContract>)) as IValidator<ICreateSelleRequestContract>,
                        new CreateSelleUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);

        [HttpGet]
        [Route("GetAllSells")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSells([FromQuery] GetAllSellsHttpQuery body) =>
            await new ConvertedGetAllSellsOnHttpContext(
                new LoggedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllSellsRequestContract>)) as ILogger<IGetAllSellsRequestContract>,
                    new ValidatedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllSellsRequestContract>)) as IValidator<IGetAllSellsRequestContract>,
                        new GetAllSellsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);

        
        [HttpGet]
        [Route("GetAllSellDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSellDetails([FromQuery] GetAllSellDetailsHttpQuery query) =>
            await new CanonicalizedGetAllSelleDetailsRequest(
                new LoggedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>)) as ILogger<IGetAllSelleDetailsRequestContract>,
                    new ValidatedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllSelleDetailsRequestContract>)) as IValidator<IGetAllSelleDetailsRequestContract>,
                        new GetAllSelleDetailsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        
        [HttpPost]
        [Route("CreateCancellation")]
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
        [Route("GetAllCancellations")]
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
        
        [HttpGet]
        [Route("GetAllCancellationDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllCancellationDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllCancellationDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCancellationDetails([FromQuery] GetAllCancellationDetailsHttpQuery query) =>
            await new CanonicalizedGetAllCancellationDetailsRequest(
                    new LoggedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllCancellationDetailsRequestContract>)) as ILogger<IGetAllCancellationDetailsRequestContract>,
                        new ValidatedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(
                            _services.GetService(typeof(IValidator<IGetAllCancellationDetailsRequestContract>)) as IValidator<IGetAllCancellationDetailsRequestContract>,
                            new GetAllCancellationDetailsUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        
        [HttpGet]
        [Route("GetAllPlacesOfStorageItemOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnUnitSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnUnitErrorResultContract))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnUnit([FromQuery] GetAllPlacesOfStorageItemOnUnitHttpQuery body) =>
            await new CanonicalizedGetAllPlacesOfStorageItemOnUnitRequest(
                new LoggedPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract, IGetAllPlacesOfStorageItemOnUnitResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>)) as ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>,
                    new ValidatedPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract, IGetAllPlacesOfStorageItemOnUnitResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllPlacesOfStorageItemOnUnitRequestContract>)) as IValidator<IGetAllPlacesOfStorageItemOnUnitRequestContract>,
                        new GetAllPlacesOfStorageItemOnUnitUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        
        [HttpGet]
        [Route("GetAllPlacesOfStorageItemOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnInstanceSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnInstanceErrorResultContract))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnInstance([FromQuery] GetAllPlacesOfStorageItemOnInstanceHttpQuery body) =>
            await new CanonicalizedGetAllPlacesOfStorageItemOnInstanceRequest(
                    new LoggedPipeNode<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>)) as ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>,
                        new ValidatedPipeNode<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>(
                            _services.GetService(typeof(IValidator<IGetAllPlacesOfStorageItemOnInstanceContract>)) as IValidator<IGetAllPlacesOfStorageItemOnInstanceContract>,
                            new GetAllPlacesOfStorageItemOnInstanceUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        
        [HttpGet]
        [Route("GetAllStorageItemsOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnInstanceSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnInstanceErrorResultContract))] 
        public async Task<IActionResult> GetAllStorageItemsOnInstance([FromQuery] GetAllStorageItemsOnInstanceHttpQuery body) =>
            await new CanonicalizedGetAllStorageItemsOnInstanceRequest(
                    new LoggedPipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemsOnInstanceContract>)) as ILogger<IGetAllStorageItemsOnInstanceContract>,
                        new ValidatedPipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(
                            _services.GetService(typeof(IValidator<IGetAllStorageItemsOnInstanceContract>)) as IValidator<IGetAllStorageItemsOnInstanceContract>,
                            new GetAllStorageItemsOnInstanceUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        
        [HttpGet]
        [Route("GetAllStorageItemsOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnUnitSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnUnitErrorResultContract))] 
        public async Task<IActionResult> GetAllStorageItemsOnUnit([FromQuery] GetAllStorageItemsOnUnitHttpQuery body) =>
            await new CanonicalizedGetAllStorageItemsOnUnitRequest(
                new LoggedPipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllStorageItemsOnUnitContract>)) as ILogger<IGetAllStorageItemsOnUnitContract>,
                    new ValidatedPipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllStorageItemsOnUnitContract>)) as IValidator<IGetAllStorageItemsOnUnitContract>,
                        new GetAllStorageItemsOnUnitUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
    }
}