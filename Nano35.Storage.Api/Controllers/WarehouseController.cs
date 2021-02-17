using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.HttpContext;
using Nano35.Storage.Api.Requests.CreateCancellation;
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.CreateMove;
using Nano35.Storage.Api.Requests.CreateSelle;
using Nano35.Storage.Api.Requests.GetAllComings;
using Nano35.Storage.Api.Requests.GetComingDetailsById;
using CreateCancellationHttpContext = Nano35.Storage.Api.HttpContext.CreateCancellationHttpContext;
using CreateComingHttpContext = Nano35.Storage.Api.HttpContext.CreateComingHttpContext;
using CreateMoveHttpContext = Nano35.Storage.Api.HttpContext.CreateMoveHttpContext;
using GetAllComingsHttpContext = Nano35.Storage.Api.HttpContext.GetAllComingsHttpContext;
using GetComingDetailsByIdHttpContext = Nano35.Storage.Api.HttpContext.GetComingDetailsByIdHttpContext;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public WarehouseController(
            IServiceProvider services)
        {
            _services = services;
        }

        /// <summary>
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()';
        /// 3. Response pattern match of pipeline response;
        /// </summary>
        [HttpPost]
        [Route("CreateComing")]
        public async Task<IActionResult> CreateComing(
            [FromHeader] CreateComingHttpContext.CreateComingHeader header,
            [FromBody] CreateComingHttpContext.CreateComingBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateComingRequest>)_services.GetService(typeof(ILogger<LoggedCreateComingRequest>));
            
            var message = new CreateComingHttpContext.CreateComingRequest()
            {
                NewId = header.NewId,
                InstanceId = header.InstanceId,
                Number = body.Number,
                UnitId = body.UnitId,
                Comment = body.Comment,
                Details = body.Details,
                ClientId = body.ClientId,
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateComingRequest(logger,
                    new ValidatedCreateComingRequest(
                        new CreateComingRequest(bus)
                    )).Ask(message);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateComingSuccessResultContract success => Ok(),
                ICreateComingErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetComingDetailsById")]
        public async Task<IActionResult> GetComingDetailsById(
            [FromQuery] GetComingDetailsByIdHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetComingDetailsByIdLogger>)_services.GetService(typeof(ILogger<GetComingDetailsByIdLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetComingDetailsByIdLogger(logger,
                    new GetComingDetailsByIdValidator(
                        new GetComingDetailsByIdRequest(bus)
                    )).Ask(query);
            
            // Check response of get all instances request
            return result switch
            {
                IGetComingDetailsByIdSuccessResultContract success => Ok(success),
                IGetComingDetailsByIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpGet]
        [Route("GetAllComings")]
        public async Task<IActionResult> GetAllComings(
            [FromHeader] GetAllComingsHttpContext.GetAllComingsHeader header,
            [FromQuery] GetAllComingsHttpContext.GetAllComingsQuery query)
        {
            var request = new GetAllComingsHttpContext.GetAllComingsRequest()
            {
                InstanceId = header.InstanceId,
                StorageItemId = query.StorageItemId,
                UnitId = query.UnitId,
            };
            
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllComingsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllComingsRequest>));
            
            var result = 
                await new LoggedGetAllComingsRequest(logger,
                    new ValidatedGetAllComingsRequest(
                        new GetAllComingsRequest(bus)
                    )).Ask(request);
            
            return result switch
            {
                IGetAllComingsSuccessResultContract success => Ok(success.Data),
                IGetAllComingsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPost]
        [Route("CreateMove")]
        public async Task<IActionResult> CreateMove(
            [FromHeader] CreateMoveHttpContext.CreateMoveHeader header,
            [FromBody] CreateMoveHttpContext.CreateMoveBody body)
        {
            var request = new CreateMoveHttpContext.CreateMoveRequest()
            {
                NewId = header.NewId,
                InstanceId = header.InstanceId,
                FromUnitId = body.FromUnitId,
                ToUnitId = body.ToUnitId,
                Details = body.Details,
                Number = body.Number,
            };
            
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateMoveRequest>)_services.GetService(typeof(ILogger<LoggedCreateMoveRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateMoveRequest(logger,
                    new ValidatedCreateMoveRequest(
                        new CreateMoveRequest(bus)
                    )).Ask(request);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateMoveSuccessResultContract success => Ok(),
                ICreateMoveErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPost]
        [Route("CreateSelle")]
        public async Task<IActionResult> CreateSelle(
            [FromHeader] CreateSelleHttpContext.CreateSelleHeader header,
            [FromBody] CreateSelleHttpContext.CreateSelleBody body)
        {
            var request = new CreateSelleHttpContext.CreateSelleRequest()
            {
                Details = body.Details,
                UnitId = body.UnitId,
                InstanceId = header.InstanceId,
                NewId = header.NewId,
                Number = body.Number,
                ClientId = body.ClientId,
            };
            
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateSelleRequest>)_services.GetService(typeof(ILogger<LoggedCreateSelleRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateSelleRequest(logger,
                    new ValidatedCreateSelleRequest(
                        new CreateSelleRequest(bus)
                    )).Ask(request);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateSelleSuccessResultContract success => Ok(),
                ICreateSelleErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllSells")]
        public async Task<IActionResult> GetAllSells()
        {
            
        }
        
        [HttpPost]
        [Route("CreateCancellation")]
        public async Task<IActionResult> CreateCancellation(
            [FromHeader] CreateCancellationHttpContext.CreateCancellationHeader header,
            [FromBody] CreateCancellationHttpContext.CreateCancellationBody body)
        {
            var request = new CreateCancellationHttpContext.CreateCancellationRequestContract()
            {
                Comment = body.Comment,
                Details = body.Details,
                InstanceId = header.InstanceId,
                NewId = header.NewId,
                Number = body.Number,
                UnitId = body.UnitId
            };
            
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateCancellationRequest>)_services.GetService(typeof(ILogger<LoggedCreateCancellationRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateCancellationRequest(logger,
                    new ValidatedCreateCancellationRequest(
                        new CreateCancellationRequest(bus)
                    )).Ask(request);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateCancellationSuccessResultContract success => Ok(),
                ICreateCancellationErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}