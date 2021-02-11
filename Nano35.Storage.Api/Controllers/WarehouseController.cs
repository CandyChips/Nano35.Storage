using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.CreateCancelation;
using Nano35.Storage.Api.Requests.CreateComing;
using Nano35.Storage.Api.Requests.CreateMove;
using Nano35.Storage.Api.Requests.CreateSalle;
using Nano35.Storage.Api.Requests.CreateStorageItem;
using Nano35.Storage.Api.Requests.GetAllStorageItemConditions;
using Nano35.Storage.Api.Requests.GetAllStorageItems;
using Nano35.Storage.Api.Requests.GetStorageItemById;
using Nano35.Storage.HttpContext;

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
        [Route("CreateCancellation")]
        public async Task<IActionResult> CreateCancellation(
            [FromBody] CreateCancelationHttpContext.Body body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<CreateCancelationLogger>)_services.GetService(typeof(ILogger<CreateCancelationLogger>));

            var message = new CreateCancelationRequestContract
            {
                NewId = body.NewId,
                IntsanceId = body.IntsanceId,
                Number = body.Number,
                UnitId = body.UnitId,
                Comment = body.Comment,
                Details = body.Details,
            };
            
            // Send request to pipeline
            var result = 
                await new CreateCancelationLogger(logger,
                    new CreateCancelationValidator(
                        new CreateCancelationRequest(bus)
                    )).Ask(message);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateCancelationSuccessResultContract success => Ok(),
                ICreateCancelationErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateComing")]
        public async Task<IActionResult> CreateComing(
            [FromBody] CreateComingHttpContext.Body command)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<CreateComingLogger>)_services.GetService(typeof(ILogger<CreateComingLogger>));
            
            var message = new CreateComingRequestContract()
            {
                NewId = command.NewId,
                InstanceId = command.InstanceId,
                Number = command.Number,
                UnitId = command.UnitId,
                Comment = command.Comment,
                Details = command.Details,
                ClientId = command.ClientId,
            };
            
            // Send request to pipeline
            var result = 
                await new CreateComingLogger(logger,
                    new CreateComingValidator(
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
        
        [HttpPost]
        [Route("CreateMove")]
        public async Task<IActionResult> CreateMove(
            [FromBody] CreateMoveHttpContext command)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<CreateMoveLogger>)_services.GetService(typeof(ILogger<CreateMoveLogger>));
            
            // Send request to pipeline
            var result = 
                await new CreateMoveLogger(logger,
                    new CreateMoveValidator(
                        new CreateMoveRequest(bus)
                    )).Ask(command);
            
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
        [Route("CreateSalle")]
        public async Task<IActionResult> CreateSalle(
            [FromBody] CreateSalleHttpContext command)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<CreateSalleLogger>)_services.GetService(typeof(ILogger<CreateSalleLogger>));
            
            // Send request to pipeline
            var result = 
                await new CreateSalleLogger(logger,
                    new CreateSalleValidator(
                        new CreateSalleRequest(bus)
                    )).Ask(command);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateSalleSuccessResultContract success => Ok(),
                ICreateSalleErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}