using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemsController : ControllerBase
    {
        private readonly ILogger<StorageItemsController> _logger;
        private readonly IMediator _mediator;

        public StorageItemsController(
            ILogger<StorageItemsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    
        [HttpGet]
        [Route("GetAllStorageItems")]
        public async Task<IActionResult> GetAllStorageItems(
            [FromQuery] Guid instanceId)
        {
            var request = new GetAllStorageItemsQuery()
            {
                InstanceId = instanceId
            };
            
            var result = await _mediator.Send(request);

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
            var request = new GetAllStorageItemConditionsQuery();
            
            var result = await _mediator.Send(request);

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
            [FromQuery] Guid id)
        {
            var request = new GetStorageItemByIdQuery()
            {
                Id = id
            };
            
            var result = await _mediator.Send(request);

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
            [FromBody] CreateStorageItemCommand command)
        {
            var result = await _mediator.Send(command);

            return result switch
            {
                ICreateStorageItemSuccessResultContract => Ok(),
                ICreateStorageItemErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}