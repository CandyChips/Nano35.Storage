using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.HttpContext.storage;
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
    public class StorageItemsController :
        ControllerBase
    {
        private readonly IServiceProvider _services;

        public StorageItemsController(IServiceProvider services) { _services = services; }
        
        [HttpGet]
        [Route("GetAllStorageItems")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItems(
            [FromQuery] GetAllStorageItemsQuery header)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllStorageItemsRequest>) _services.GetService(typeof(ILogger<LoggedGetAllStorageItemsRequest>));

            return await new ConvertedGetAllStorageItemsOnHttpContext(
                        new LoggedGetAllStorageItemsRequest(logger,
                            new ValidatedGetAllStorageItemsRequest(
                                new GetAllStorageItemsUseCase(bus)))).Ask(header);
        }
    
        [HttpGet]
        [Route("GetAllStorageItemConditions")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemConditionsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemConditionsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItemConditions()
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllStorageItemConditionsRequest>) _services.GetService(typeof(ILogger<LoggedGetAllStorageItemConditionsRequest>));

            return await new ConvertedGetAllStorageItemConditionsOnHttpContext(
                        new LoggedGetAllStorageItemConditionsRequest(logger,
                            new GetAllStorageItemConditionsUseCase(bus))).Ask(
                                new GetAllStorageItemConditionsHttpQuery());
        }
    
        [HttpGet]
        [Route("GetStorageItemById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetStorageItemByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetStorageItemByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetStorageItemById(
            [FromQuery] GetStorageItemByIdHttpQuery query)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetStorageItemByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetStorageItemByIdRequest>));

            return await new ConvertedGetStorageItemByIdOnHttpContext(
                        new LoggedGetStorageItemByIdRequest(logger,
                            new ValidatedGetStorageItemByIdRequest(
                                new GetStorageItemByIdUseCase(bus)))).Ask(query);
        }
        
        [HttpPost]
        [Route("CreateStorageItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateStorageItemSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateStorageItemErrorHttpResponse))] 
        public async Task<IActionResult> CreateStorageItem(
            [FromBody] CreateStorageItemHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateStorageItemRequest>) _services.GetService(typeof(ILogger<LoggedCreateStorageItemRequest>));

            return await new ConvertedCreateStorageItemOnHttpContext(
                        new LoggedCreateStorageItemRequest(logger,
                            new ValidatedCreateStorageItemRequest(
                                new CreateStorageItemUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemArticle")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemArticleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemArticleErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemArticle(
            [FromBody] UpdateStorageItemArticleHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemArticleRequest>) _services.GetService(typeof(ILogger<LoggedUpdateStorageItemArticleRequest>));

            return await new ConvertedUpdateStorageItemArticleOnHttpContext(
                        new LoggedUpdateStorageItemArticleRequest(logger,  
                            new ValidatedUpdateStorageItemArticleRequest(
                                new UpdateStorageItemArticleUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemComment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemCommentErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemComment(
            [FromBody] UpdateStorageItemCommentHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemCommentRequest>) _services.GetService(typeof(ILogger<LoggedUpdateStorageItemCommentRequest>));

            return await new ConvertedUpdateStorageItemCommentOnHttpContext(
                        new LoggedUpdateStorageItemCommentRequest(logger,  
                            new ValidatedUpdateStorageItemCommentRequest(
                                new UpdateStorageItemCommentUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemCondition")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemConditionSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemConditionErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemCondition(
            [FromBody] UpdateStorageItemConditionHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemConditionRequest>) _services.GetService(typeof(ILogger<LoggedUpdateStorageItemConditionRequest>));

            return await new ConvertedUpdateStorageItemConditionOnHttpContext(
                        new LoggedUpdateStorageItemConditionRequest(logger,  
                            new ValidatedUpdateStorageItemConditionRequest(
                                new UpdateStorageItemConditionUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemHiddenComment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemHiddenCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemHiddenCommentErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemHiddenComment(
            [FromBody] UpdateStorageItemHiddenCommentHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemHiddenCommentRequest>) _services.GetService(typeof(ILogger<LoggedUpdateStorageItemHiddenCommentRequest>));

            return await new ConvertedUpdateStorageItemHiddenCommentOnHttpContext(
                        new LoggedUpdateStorageItemHiddenCommentRequest(logger,  
                            new ValidatedUpdateStorageItemHiddenCommentRequest(
                                new UpdateStorageItemHiddenCommentUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemPurchasePrice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemPurchasePriceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemPurchasePriceErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemPurchasePrice(
            [FromBody] UpdateStorageItemPurchasePriceHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemPurchasePriceRequest>) _services.GetService(typeof(ILogger<LoggedUpdateStorageItemPurchasePriceRequest>));

            return await new ConvertedUpdateStorageItemPurchasePriceOnHttpContext(
                        new LoggedUpdateStorageItemPurchasePriceRequest(logger,  
                            new ValidatedUpdateStorageItemPurchasePriceRequest(
                                new UpdateStorageItemPurchasePriceUseCase(bus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemRetailPrice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemRetailPriceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemRetailPriceErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemRetailPrice(
            [FromBody] UpdateStorageItemRetailPriceHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemRetailPriceRequest>) _services.GetService(typeof(ILogger<LoggedUpdateStorageItemRetailPriceRequest>));

            return await new ConvertedUpdateStorageItemRetailPriceOnHttpContext(
                        new LoggedUpdateStorageItemRetailPriceRequest(logger,  
                            new ValidatedUpdateStorageItemRetailPriceRequest(
                                new UpdateStorageItemRetailPriceUseCase(bus)))).Ask(body);
        }
    }
}