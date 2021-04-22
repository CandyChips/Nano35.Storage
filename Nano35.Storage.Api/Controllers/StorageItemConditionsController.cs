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
using Nano35.Storage.Api.Requests.CreateStorageItem;
using Nano35.Storage.Api.Requests.GetAllStorageItemConditions;
using Nano35.Storage.Api.Requests.GetAllStorageItems;
using Nano35.Storage.Api.Requests.GetStorageItemById;
using Nano35.Storage.Api.Requests.UpdateStorageItemArticle;
using Nano35.Storage.Api.Requests.UpdateStorageItemComment;
using Nano35.Storage.Api.Requests.UpdateStorageItemCondition;
using Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment;
using Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice;
using Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemConditionsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;
        public StorageItemConditionsController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemConditionsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemConditionsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItemConditions()
        {
            return await new CanonicalizedGetAllStorageItemConditionsRequest(
                new LoggedPipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllStorageItemConditionsRequestContract>)) as ILogger<IGetAllStorageItemConditionsRequestContract>,
                    new ValidatedPipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemConditionsRequestContract>)) as IValidator<IGetAllStorageItemConditionsRequestContract>,
                        new GetAllStorageItemConditionsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllStorageItemConditionsHttpQuery());
        }
    }
}