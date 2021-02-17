using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.CreateStorageItem;
using Nano35.Storage.Api.Requests.GetAllStorageItemConditions;
using Nano35.Storage.Api.Requests.GetAllStorageItems;
using Nano35.Storage.Api.Requests.GetStorageItemById;
using Nano35.Storage.HttpContext;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public StorageItemsController(
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
        [HttpGet]
        [Route("GetAllStorageItems")]
        public async Task<IActionResult> GetAllStorageItems(
            [FromHeader] GetAllStorageItemsHttpContext.GetAllStorageItemsHeader header,
            [FromQuery] GetAllStorageItemsHttpContext.GetAllStorageItemsQuery query)
        {
            var request = new GetAllStorageItemsHttpContext.GetAllStorageItemsRequest()
            {
                InstanceId = query.InstanceId
            };
            
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllStorageItemsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllStorageItemsRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllStorageItemsRequest(logger,
                    new ValidatedGetAllStorageItemsRequest(
                        new GetAllStorageItemsRequest(bus)
                        )).Ask(request);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                IGetAllStorageItemsSuccessResultContract success => Ok(success.Data),
                IGetAllStorageItemsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllStorageItemConditions")]
        public async Task<IActionResult> GetAllStorageItemConditions()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllStorageItemConditionsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllStorageItemConditionsRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllStorageItemConditionsRequest(logger,
                    new GetAllStorageItemConditionsRequest(bus)
                ).Ask(new GetAllStorageItemConditionsHttpContext());
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                IGetAllStorageItemConditionsSuccessResultContract success => Ok(success.Data),
                IGetAllStorageItemConditionsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetStorageItemById")]
        public async Task<IActionResult> GetStorageItemById(
            [FromQuery] GetStorageItemByIdHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetStorageItemByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetStorageItemByIdRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetStorageItemByIdRequest(logger,
                    new ValidatedGetStorageItemByIdRequest(
                        new GetStorageItemByIdRequest(bus)
                        )).Ask(query);
            
            // Check response of get all instances request
            return result switch
            {
                IGetStorageItemByIdSuccessResultContract success => Ok(success.Data),
                IGetStorageItemByIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateStorageItem")]
        public async Task<IActionResult> CreateStorageItem(
            [FromBody] CreateStorageItemHttpContext command)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateStorageItemRequest>)_services.GetService(typeof(ILogger<LoggedCreateStorageItemRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateStorageItemRequest(logger,
                    new ValidatedCreateStorageItemRequest(
                        new CreateStorageItemRequest(bus)
                        )).Ask(command);
            
            // Check response of get all instances request
            // You can check result by result contracts
            return result switch
            {
                ICreateStorageItemSuccessResultContract => Ok(),
                ICreateStorageItemErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}