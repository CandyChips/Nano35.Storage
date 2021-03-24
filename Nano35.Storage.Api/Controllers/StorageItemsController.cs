using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.instance;
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

        public StorageItemsController(
            IServiceProvider services)
        {
            _services = services;
        }
        
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

            var request = new GetAllStorageItemsRequestContract()
            {
                InstanceId = header.InstanceId
            };
            
            var result = 
                await new LoggedGetAllStorageItemsRequest(logger,
                    new ValidatedGetAllStorageItemsRequest(
                        new GetAllStorageItemsRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetAllStorageItemsSuccessResultContract success => Ok(success),
                IGetAllStorageItemsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new GetAllStorageItemConditionsRequestContract();
            
            var result = 
                await new LoggedGetAllStorageItemConditionsRequest(logger,
                    new GetAllStorageItemConditionsRequest(bus))
                    .Ask(request);
            
            return result switch
            {
                IGetAllStorageItemConditionsSuccessResultContract success => Ok(success),
                IGetAllStorageItemConditionsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new GetStorageItemByIdRequestContract()
            {
                Id = query.Id
            };
            
            var result = 
                await new LoggedGetStorageItemByIdRequest(logger,
                    new ValidatedGetStorageItemByIdRequest(
                        new GetStorageItemByIdRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetStorageItemByIdSuccessResultContract success => Ok(success),
                IGetStorageItemByIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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
                                new CreateStorageItemRequest(bus)))).Ask(body);
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

            var request = new UpdateStorageItemArticleRequestContract()
            {
                ArticleId = body.ArticleId,
                Id = body.Id
            };
            
            var result =
                await new LoggedUpdateStorageItemArticleRequest(logger,  
                    new ValidatedUpdateStorageItemArticleRequest(
                        new UpdateStorageItemArticleRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateStorageItemArticleSuccessResultContract => Ok(),
                IUpdateStorageItemArticleErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateStorageItemCommentRequestContract()
            {
                Comment = body.Comment,
                Id = body.Id
            };
            
            var result =
                await new LoggedUpdateStorageItemCommentRequest(logger,  
                    new ValidatedUpdateStorageItemCommentRequest(
                        new UpdateStorageItemCommentRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateStorageItemCommentSuccessResultContract => Ok(),
                IUpdateStorageItemCommentErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateStorageItemConditionRequestContract()
            {
                ConditionId = body.ConditionId,
                Id = body.Id,
            };
            
            var result =
                await new LoggedUpdateStorageItemConditionRequest(logger,  
                    new ValidatedUpdateStorageItemConditionRequest(
                        new UpdateStorageItemConditionRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateStorageItemConditionSuccessResultContract => Ok(),
                IUpdateStorageItemConditionErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateStorageItemHiddenCommentRequestContract()
            {
                HiddenComment = body.HiddenComment,
                Id = body.Id,
            };
            
            var result =
                await new LoggedUpdateStorageItemHiddenCommentRequest(logger,  
                    new ValidatedUpdateStorageItemHiddenCommentRequest(
                        new UpdateStorageItemHiddenCommentRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateStorageItemHiddenCommentSuccessResultContract => Ok(),
                IUpdateStorageItemHiddenCommentErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateStorageItemPurchasePriceRequestContract()
            {
                PurchasePrice = body.PurchasePrice,
                Id = body.Id
            };
            
            var result =
                await new LoggedUpdateStorageItemPurchasePriceRequest(logger,  
                    new ValidatedUpdateStorageItemPurchasePriceRequest(
                        new UpdateStorageItemPurchasePriceRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateStorageItemPurchasePriceSuccessResultContract => Ok(),
                IUpdateStorageItemPurchasePriceErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateStorageItemRetailPriceRequestContract()
            {
                RetailPrice = body.RetailPrice,
                Id = body.Id
            };
            
            var result =
                await new LoggedUpdateStorageItemRetailPriceRequest(logger,  
                    new ValidatedUpdateStorageItemRetailPriceRequest(
                        new UpdateStorageItemRetailPriceRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IUpdateStorageItemRetailPriceSuccessResultContract => Ok(),
                IUpdateStorageItemRetailPriceErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    }
}