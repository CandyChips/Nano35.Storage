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

        public WarehouseController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("Coming")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateComingSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateComingErrorHttpResponse))] 
        public async Task<IActionResult> CreateComing(
            [FromBody] CreateComingHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateComingHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateComingHttpBody>)) as IValidator<CreateComingHttpBody>, 
                    new ConvertedCreateComingOnHttpContext(
                        new LoggedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(
                            _services.GetService(typeof(ILogger<ICreateComingRequestContract>)) as ILogger<ICreateComingRequestContract>,
                            new CreateComingUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpGet]
        [Route("Comings")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllComingsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllComingsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllComings(
            [FromHeader] GetAllComingsHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllComingsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllComingsHttpQuery>)) as IValidator<GetAllComingsHttpQuery>, 
                    new ConvertedGetAllComingsOnHttpContext(
                        new LoggedPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllComingsRequestContract>)) as ILogger<IGetAllComingsRequestContract>,
                            new GetAllComingsUseCase(_services.GetService(typeof(IBus)) as IBus)))).Ask(query);
        }
        
        [HttpGet]
        [Route("ComingDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetComingDetailsByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetComingDetailsByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetComingDetails(
            [FromQuery] GetAllComingDetailsHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllComingDetailsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllComingDetailsHttpQuery>)) as IValidator<GetAllComingDetailsHttpQuery>, 
                    new ConvertedGetAllComingDetailsOnHttpContext(
                        new LoggedPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllComingDetailsRequestContract>)) as ILogger<IGetAllComingDetailsRequestContract>,
                            new GetAllComingDetailsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(query);
        }
        
        [HttpPost]
        [Route("Move")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateMoveSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateMoveErrorHttpResponse))] 
        public async Task<IActionResult> CreateMove(
            [FromBody] CreateMoveHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateMoveHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateMoveHttpBody>)) as IValidator<CreateMoveHttpBody>, 
                    new ConvertedCreateMoveOnHttpContext(
                        new LoggedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(
                            _services.GetService(typeof(ILogger<ICreateMoveRequestContract>)) as ILogger<ICreateMoveRequestContract>,
                            new CreateMoveUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }

        [HttpGet]
        [Route("Moves")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMovesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMovesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoves(
            [FromQuery] GetAllMovesHttpQuery body)
        {
            return await 
                new ValidatedPipeNode<GetAllMovesHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllMovesHttpQuery>)) as IValidator<GetAllMovesHttpQuery>, 
                    new ConvertedGetAllMovesOnHttpContext(
                        new LoggedPipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllMovesRequestContract>)) as ILogger<IGetAllMovesRequestContract>,
                            new GetAllMovesUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpGet]
        [Route("MoveDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllMoveDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllMoveDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllMoveDetails(
            [FromQuery] GetAllMoveDetailsHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllMoveDetailsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllMoveDetailsHttpQuery>)) as IValidator<GetAllMoveDetailsHttpQuery>, 
                    new ConvertedGetAllMoveDetailsOnHttpContext(
                        new LoggedPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllMoveDetailsRequestContract>)) as ILogger<IGetAllMoveDetailsRequestContract>,
                            new GetAllMoveDetailsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(query);
        }
        
        [HttpPost]
        [Route("Selle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateSelleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateSelleErrorHttpResponse))] 
        public async Task<IActionResult> CreateSelle(
            [FromBody] CreateSelleHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateSelleHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateSelleHttpBody>)) as IValidator<CreateSelleHttpBody>, 
                    new ConvertedCreateSelleOnHttpContext(
                        new LoggedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                            _services.GetService(typeof(ILogger<ICreateSelleRequestContract>)) as ILogger<ICreateSelleRequestContract>,
                            new CreateSelleUseCase(_services.GetService(typeof(IBus)) as IBus)))).Ask(body);
        }

        [HttpGet]
        [Route("Sells")]
        public async Task<IActionResult> GetAllSells(
            [FromQuery] GetAllSellsHttpQuery body)
        {
            return await 
                new ValidatedPipeNode<GetAllSellsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllSellsHttpQuery>)) as IValidator<GetAllSellsHttpQuery>, 
                    new ConvertedGetAllSellsOnHttpContext(
                        new LoggedPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllSellsRequestContract>)) as ILogger<IGetAllSellsRequestContract>,
                            new GetAllSellsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }

        
        [HttpGet]
        [Route("SellDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllSellDetails(
            [FromQuery] GetAllSellDetailsHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllSellDetailsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllSellDetailsHttpQuery>)) as IValidator<GetAllSellDetailsHttpQuery>, 
                    new ConvertedGetAllSelleDetailsOnHttpContext(
                        new LoggedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>)) as ILogger<IGetAllSelleDetailsRequestContract>,
                           new GetAllSelleDetailsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(query);
        }
        
        [HttpPost]
        [Route("Cancellation")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCancellationSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCancellationErrorHttpResponse))] 
        public async Task<IActionResult> CreateCancellation(
            [FromBody] CreateCancellationHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateCancellationHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateCancellationHttpBody>)) as IValidator<CreateCancellationHttpBody>, 
                    new ConvertedCreateCancellationOnHttpContext(
                        new LoggedPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(
                            _services.GetService(typeof(ILogger<ICreateCancellationRequestContract>)) as ILogger<ICreateCancellationRequestContract>,
                            new CreateCancellationUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }

        [HttpGet]
        [Route("Cancellations")]
        public async Task<IActionResult> GetAllCancellations(
            [FromQuery] GetAllCancellationsHttpQuery body)
        {
            return await 
                new ValidatedPipeNode<GetAllCancellationsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllCancellationsHttpQuery>)) as IValidator<GetAllCancellationsHttpQuery>, 
                    new ConvertedGetAllCancellationsOnHttpContext(
                        new LoggedPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllCancellationsRequestContract>)) as ILogger<IGetAllCancellationsRequestContract>,
                            new GetAllCancellationsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpGet]
        [Route("CancellationDetails")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllSellDetailsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllSellDetailsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllCancellationDetails(
            [FromQuery] GetAllCancellationDetailsHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllCancellationDetailsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllCancellationDetailsHttpQuery>)) as IValidator<GetAllCancellationDetailsHttpQuery>, 
                    new ConvertedGetAllCancellationDetailsOnHttpContext(
                        new LoggedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllCancellationDetailsRequestContract>)) as ILogger<IGetAllCancellationDetailsRequestContract>,
                            new GetAllCancellationDetailsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(query);
        }
        
        [HttpGet]
        [Route("PlacesOfStorageItemOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnUnitSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnUnitErrorResultContract))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnUnit(
            [FromQuery] GetAllPlacesOfStorageItemOnUnitHttpQuery body)
        {
            return await 
                new ValidatedPipeNode<GetAllPlacesOfStorageItemOnUnitHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllPlacesOfStorageItemOnUnitHttpQuery>)) as IValidator<GetAllPlacesOfStorageItemOnUnitHttpQuery>, 
                    new ConvertedGetAllPlacesOfStorageItemOnUnitOnHttpContext(
                        new LoggedPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract, IGetAllPlacesOfStorageItemOnUnitResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>)) as ILogger<IGetAllPlacesOfStorageItemOnUnitRequestContract>,
                            new GetAllPlacesOfStorageItemOnUnitUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpGet]
        [Route("PlacesOfStorageItemOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllPlacesOfStorageItemOnInstanceSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllPlacesOfStorageItemOnInstanceErrorResultContract))] 
        public async Task<IActionResult> GetAllPlacesOfStorageItemOnInstance(
            [FromQuery] GetAllPlacesOfStorageItemOnInstanceHttpQuery body)
        {
            return await 
                new ValidatedPipeNode<GetAllPlacesOfStorageItemOnInstanceHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllPlacesOfStorageItemOnInstanceHttpQuery>)) as IValidator<GetAllPlacesOfStorageItemOnInstanceHttpQuery>, 
                    new ConvertedGetAllPlacesOfStorageItemOnInstanceOnHttpContext(
                        new LoggedPipeNode<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>)) as ILogger<IGetAllPlacesOfStorageItemOnInstanceContract>,
                            new GetAllPlacesOfStorageItemOnInstanceUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpGet]
        [Route("StorageItemsOnInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnInstanceSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnInstanceErrorResultContract))] 
        public async Task<IActionResult> GetAllStorageItemsOnInstance(
            [FromQuery] GetAllStorageItemsOnInstanceHttpQuery body)
        {
            return await 
                new ValidatedPipeNode<GetAllStorageItemsOnInstanceHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllStorageItemsOnInstanceHttpQuery>)) as IValidator<GetAllStorageItemsOnInstanceHttpQuery>, 
                    new ConvertedGetAllStorageItemsOnInstanceOnHttpContext(
                        new LoggedPipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllStorageItemsOnInstanceContract>)) as ILogger<IGetAllStorageItemsOnInstanceContract>,
                            new GetAllStorageItemsOnInstanceUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }
        
        [HttpGet]
        [Route("StorageItemsOnUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsOnUnitSuccessResultContract))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsOnUnitErrorResultContract))] 
        public async Task<IActionResult> GetAllStorageItemsOnUnit(
            [FromQuery] GetAllStorageItemsOnUnitHttpQuery body)
        {
            return await
                new ValidatedPipeNode<GetAllStorageItemsOnUnitHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllStorageItemsOnUnitHttpQuery>)) as IValidator<GetAllStorageItemsOnUnitHttpQuery>, 
                    new ConvertedGetAllStorageItemsOnUnitOnHttpContext(
                        new LoggedPipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllStorageItemsOnUnitContract>)) as ILogger<IGetAllStorageItemsOnUnitContract>,
                            new GetAllStorageItemsOnUnitUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
    }
}