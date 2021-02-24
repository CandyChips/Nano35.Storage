using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.CreateStorageItem;
using Nano35.Storage.Api.Requests.GetAllStorageItemConditions;
using Nano35.Storage.Api.Requests.GetAllStorageItems;
using Nano35.Storage.Api.Requests.GetComingDetailsById;
using Nano35.Storage.Api.Requests.GetStorageItemById;
using Nano35.Storage.Api.Requests.UpdateStorageItemArticle;
using Nano35.Storage.Api.Requests.UpdateStorageItemComment;
using Nano35.Storage.Api.Requests.UpdateStorageItemCondition;
using Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment;
using Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice;
using Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice;
using CreateStorageItemHttpContext = Nano35.Storage.Api.HttpContext.CreateStorageItemHttpContext;
using GetAllStorageItemConditionsHttpContext = Nano35.Storage.Api.HttpContext.GetAllStorageItemConditionsHttpContext;
using GetAllStorageItemsHttpContext = Nano35.Storage.Api.HttpContext.GetAllStorageItemsHttpContext;
using GetStorageItemByIdHttpContext = Nano35.Storage.Api.HttpContext.GetStorageItemByIdHttpContext;

namespace Nano35.Storage.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageItemsController :
        ControllerBase
    {
        public class UpdateStorageItemArticleHttpContext : IUpdateStorageItemArticleRequestContract
        {
            public Guid Id { get; set; }
            public Guid ArticleId { get; set; }
        }
        
        public class UpdateStorageItemCommentHttpContext : IUpdateStorageItemCommentRequestContract
        {
            public Guid Id { get; set; }
            public string Comment { get; set; }
        }
        public class UpdateStorageItemConditionHttpContext : IUpdateStorageItemConditionRequestContract
        {
            public Guid Id { get; set; }
            public Guid ConditionId { get; set; }
        }
        public class UpdateStorageItemHiddenCommentHttpContext : IUpdateStorageItemHiddenCommentRequestContract
        {
            public Guid Id { get; set; }
            public string HiddenComment { get; set; }
        }
        public class UpdateStorageItemPurchasePriceHttpContext : IUpdateStorageItemPurchasePriceRequestContract
        {
            public Guid Id { get; set; }
            public decimal PurchasePrice { get; set; }
        }
        public class UpdateStorageItemRetailPriceHttpContext : IUpdateStorageItemRetailPriceRequestContract
        {
            public Guid Id { get; set; }
            public decimal RetailPrice { get; set; }
        }
        
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
        
        [HttpPatch]
        [Route("UpdateStorageItemArticle")]
        public async Task<IActionResult> UpdateStorageItemArticle(
            [FromBody] UpdateStorageItemArticleHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemArticleRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemArticleRequest>));

            var result =
                await new LoggedUpdateStorageItemArticleRequest(logger,  
                    new ValidatedUpdateStorageItemArticleRequest(
                        new UpdateStorageItemArticleRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateStorageItemArticleSuccessResultContract => Ok(),
                IUpdateStorageItemArticleErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemComment")]
        public async Task<IActionResult> UpdateStorageItemComment(
            [FromBody] UpdateStorageItemCommentHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemCommentRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemCommentRequest>));

            var result =
                await new LoggedUpdateStorageItemCommentRequest(logger,  
                    new ValidatedUpdateStorageItemCommentRequest(
                        new UpdateStorageItemCommentRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateStorageItemCommentSuccessResultContract => Ok(),
                IUpdateStorageItemCommentErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemCondition")]
        public async Task<IActionResult> UpdateStorageItemCondition(
            [FromBody] UpdateStorageItemConditionHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemConditionRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemConditionRequest>));

            var result =
                await new LoggedUpdateStorageItemConditionRequest(logger,  
                    new ValidatedUpdateStorageItemConditionRequest(
                        new UpdateStorageItemConditionRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateStorageItemConditionSuccessResultContract => Ok(),
                IUpdateStorageItemConditionErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemHiddenComment")]
        public async Task<IActionResult> UpdateStorageItemHiddenComment(
            [FromBody] UpdateStorageItemHiddenCommentHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemHiddenCommentRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemHiddenCommentRequest>));

            var result =
                await new LoggedUpdateStorageItemHiddenCommentRequest(logger,  
                    new ValidatedUpdateStorageItemHiddenCommentRequest(
                        new UpdateStorageItemHiddenCommentRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateStorageItemHiddenCommentSuccessResultContract => Ok(),
                IUpdateStorageItemHiddenCommentErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemPurchasePrice")]
        public async Task<IActionResult> UpdateStorageItemPurchasePrice(
            [FromBody] UpdateStorageItemPurchasePriceHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemPurchasePriceRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemPurchasePriceRequest>));

            var result =
                await new LoggedUpdateStorageItemPurchasePriceRequest(logger,  
                    new ValidatedUpdateStorageItemPurchasePriceRequest(
                        new UpdateStorageItemPurchasePriceRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateStorageItemPurchasePriceSuccessResultContract => Ok(),
                IUpdateStorageItemPurchasePriceErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateStorageItemRetailPrice")]
        public async Task<IActionResult> UpdateStorageItemRetailPrice(
            [FromBody] UpdateStorageItemRetailPriceHttpContext body)
        {
            
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateStorageItemRetailPriceRequest>) _services
                .GetService(typeof(ILogger<LoggedUpdateStorageItemRetailPriceRequest>));

            var result =
                await new LoggedUpdateStorageItemRetailPriceRequest(logger,  
                    new ValidatedUpdateStorageItemRetailPriceRequest(
                        new UpdateStorageItemRetailPriceRequest(bus)
                    )
                ).Ask(body);
            
            return result switch
            {
                IUpdateStorageItemRetailPriceSuccessResultContract => Ok(),
                IUpdateStorageItemRetailPriceErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}