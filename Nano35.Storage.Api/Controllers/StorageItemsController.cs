using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CreateStorageItem;
using Nano35.Storage.Api.Requests.GetAllArticles;
using Nano35.Storage.Api.Requests.GetAllStorageItemConditions;
using Nano35.Storage.Api.Requests.GetAllStorageItems;
using Nano35.Storage.Api.Requests.GetStorageItemById;
using Nano35.Storage.HttpContext;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemsController : ControllerBase
    {
        private readonly IServiceProvider _services;

        public StorageItemsController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        [HttpGet]
        [Route("GetAllStorageItems")]
        public async Task<IActionResult> GetAllStorageItems(
            [FromQuery] GetAllStorageItemsHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllStorageItemsLogger>)_services.GetService(typeof(ILogger<GetAllStorageItemsLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetAllStorageItemsLogger(logger,
                    new GetAllStorageItemsValidator(
                        new GetAllStorageItemsRequest(bus))
                ).Ask(query);
            
            // Check response of get all instances request
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
            var logger = (ILogger<GetAllStorageItemConditionsLogger>)_services.GetService(typeof(ILogger<GetAllStorageItemConditionsLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetAllStorageItemConditionsLogger(logger,
                    new GetAllStorageItemConditionsRequest(bus)
                ).Ask(new GetAllStorageItemConditionsHttpContext());
            
            // Check response of get all instances request
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
            var logger = (ILogger<GetStorageItemByIdLogger>)_services.GetService(typeof(ILogger<GetStorageItemByIdLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetStorageItemByIdLogger(logger,
                    new GetStorageItemByIdValidator(
                        new GetStorageItemByIdRequest(bus))
                ).Ask(query);
            
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
            var logger = (ILogger<CreateStorageItemLogger>)_services.GetService(typeof(ILogger<CreateStorageItemLogger>));
            
            // Send request to pipeline
            var result = 
                await new CreateStorageItemLogger(logger,
                    new CreateStorageItemValidator(
                        new CreateStorageItemRequest(bus))
                ).Ask(command);
            
            // Check response of get all instances request
            return result switch
            {
                ICreateStorageItemSuccessResultContract => Ok(),
                ICreateStorageItemErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}