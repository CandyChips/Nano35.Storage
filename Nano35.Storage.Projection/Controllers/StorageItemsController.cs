using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Projection.UseCases;
using Nano35.Storage.Projection.UseCases.GetAllStorageItemConditions;
using Nano35.Storage.Projection.UseCases.PresentationGetAllStorageItems;

namespace Nano35.Storage.Projection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        
        public StorageItemsController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PresentationGetAllStorageItemsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(PresentationGetAllStorageItemsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItems(Guid instanceId)
        {
            var result = await 
                    new LoggedPipeNode<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>(_services.GetService(typeof(ILogger<IPresentationGetAllStorageItemsRequestContract>)) as ILogger<IPresentationGetAllStorageItemsRequestContract>,
                        new PresentationGetAllStorageItems(_services.GetService(typeof(IBus)) as IBus))
                .Ask(new PresentationGetAllStorageItemsRequestContract() { InstanceId = instanceId });
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    
        [HttpGet("Conditions")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemConditionsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemConditionsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItemConditions()
        {
            var result = await new LoggedPipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllStorageItemConditionsRequestContract>)) as ILogger<IGetAllStorageItemConditionsRequestContract>,
                    new GetAllStorageItemConditions(_services.GetService(typeof(IBus)) as IBus))
                .Ask(new GetAllStorageItemConditionsRequestContract());
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}