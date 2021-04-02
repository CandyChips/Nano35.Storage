using System;
using System.Threading.Tasks;
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
            var logger = (ILogger<IGetAllStorageItemsRequestContract>) _services.GetService(typeof(ILogger<IGetAllStorageItemsRequestContract>));

            return await new CanonicalizedGetAllStorageItemsRequest(
                        new LoggedPipeNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>(logger,
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
            var logger = (ILogger<IGetAllStorageItemConditionsRequestContract>) _services.GetService(typeof(ILogger<IGetAllStorageItemConditionsRequestContract>));

            return await new CanonicalizedGetAllStorageItemConditionsRequest(
                        new LoggedPipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>(logger,
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
            var logger = (ILogger<IGetStorageItemByIdRequestContract>) _services.GetService(typeof(ILogger<IGetStorageItemByIdRequestContract>));

            return await new CanonicalizedGetStorageItemByIdRequest(
                        new LoggedPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>(logger,
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
            var logger = (ILogger<ICreateStorageItemRequestContract>) _services.GetService(typeof(ILogger<ICreateStorageItemRequestContract>));

            return await new CanonicalizedCreateStorageItemRequest(
                        new LoggedPipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(logger,
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
            var logger = (ILogger<IUpdateStorageItemArticleRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemArticleRequestContract>));

            return await new CanonicalizedUpdateStorageItemArticleRequest(
                        new LoggedPipeNode<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>(logger,  
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
            var logger = (ILogger<IUpdateStorageItemCommentRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemCommentRequestContract>));

            return await new CanonicalizedUpdateStorageItemCommentRequest(
                        new LoggedPipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>(logger,  
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
            var logger = (ILogger<IUpdateStorageItemConditionRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemConditionRequestContract>));

            return await new CanonicalizedUpdateStorageItemConditionRequest(
                        new LoggedPipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>(logger,  
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
            var logger = (ILogger<IUpdateStorageItemHiddenCommentRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemHiddenCommentRequestContract>));

            return await new CanonicalizedUpdateStorageItemHiddenCommentRequest(
                        new LoggedPipeNode<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>(logger,  
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
            var logger = (ILogger<IUpdateStorageItemPurchasePriceRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemPurchasePriceRequestContract>));

            return await new CanonicalizedUpdateStorageItemPurchasePriceRequest(
                        new LoggedPipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>(logger,  
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
            var logger = (ILogger<IUpdateStorageItemRetailPriceRequestContract>) _services.GetService(typeof(ILogger<IUpdateStorageItemRetailPriceRequestContract>));

            return await new CanonicalizedUpdateStorageItemRetailPriceRequest(
                        new LoggedPipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>(logger,  
                            new ValidatedUpdateStorageItemRetailPriceRequest(
                                new UpdateStorageItemRetailPriceUseCase(bus)))).Ask(body);
        }
    }
}