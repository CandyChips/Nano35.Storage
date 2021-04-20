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
            return await 
                new ValidatedPipeNode<GetAllStorageItemsQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllStorageItemsQuery>)) as IValidator<GetAllStorageItemsQuery>,
                    new ConvertedGetAllStorageItemsOnHttpContext(
                        new LoggedPipeNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllStorageItemsRequestContract>)) as ILogger<IGetAllStorageItemsRequestContract>,
                            new GetAllStorageItemsUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(header);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetStorageItemByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetStorageItemByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetStorageItemById(Guid id)
        {
            return await 
                new ValidatedPipeNode<Guid, IActionResult>(
                    _services.GetService(typeof(IValidator<Guid>)) as IValidator<Guid>, 
                    new ConvertedGetStorageItemByIdOnHttpContext(
                        new LoggedPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>(
                            _services.GetService(typeof(ILogger<IGetStorageItemByIdRequestContract>)) as ILogger<IGetStorageItemByIdRequestContract>,
                            new GetStorageItemByIdUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(id);
        }
        
        [HttpPost]
        [Route("StorageItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateStorageItemSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateStorageItemErrorHttpResponse))] 
        public async Task<IActionResult> CreateStorageItem(
            [FromBody] CreateStorageItemHttpBody body)
        {
            return await
                new ValidatedPipeNode<CreateStorageItemHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateStorageItemHttpBody>)) as IValidator<CreateStorageItemHttpBody>,
                    new ConvertedCreateStorageItemOnHttpContext(
                        new LoggedPipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(
                            _services.GetService(typeof(ILogger<ICreateStorageItemRequestContract>)) as ILogger<ICreateStorageItemRequestContract>,
                            new CreateStorageItemUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpPatch]
        [Route("Article")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemArticleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemArticleErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemArticle(
            [FromBody] UpdateStorageItemArticleHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateStorageItemArticleHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateStorageItemArticleHttpBody>)) as IValidator<UpdateStorageItemArticleHttpBody>, 
                    new ConvertedUpdateStorageItemArticleOnHttpContext(
                        new LoggedPipeNode<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateStorageItemArticleRequestContract>)) as ILogger<IUpdateStorageItemArticleRequestContract>,
                            new UpdateStorageItemArticleUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpPatch]
        [Route("Comment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemCommentErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemComment(
            [FromBody] UpdateStorageItemCommentHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateStorageItemCommentHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateStorageItemCommentHttpBody>)) as IValidator<UpdateStorageItemCommentHttpBody>, 
                    new ConvertedUpdateStorageItemCommentOnHttpContext(
                        new LoggedPipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateStorageItemCommentRequestContract>)) as ILogger<IUpdateStorageItemCommentRequestContract>,
                            new UpdateStorageItemCommentUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpPatch]
        [Route("Condition")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemConditionSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemConditionErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemCondition(
            [FromBody] UpdateStorageItemConditionHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateStorageItemConditionHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateStorageItemConditionHttpBody>)) as IValidator<UpdateStorageItemConditionHttpBody>, 
                    new ConvertedUpdateStorageItemConditionOnHttpContext(
                        new LoggedPipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateStorageItemConditionRequestContract>)) as ILogger<IUpdateStorageItemConditionRequestContract>,
                            new UpdateStorageItemConditionUseCase(_services.GetService(typeof(IBus)) as IBus)))).Ask(body);
        }
        
        [HttpPatch]
        [Route("HiddenComment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemHiddenCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemHiddenCommentErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemHiddenComment(
            [FromBody] UpdateStorageItemHiddenCommentHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateStorageItemHiddenCommentHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateStorageItemHiddenCommentHttpBody>)) as IValidator<UpdateStorageItemHiddenCommentHttpBody>,  
                    new ConvertedUpdateStorageItemHiddenCommentOnHttpContext(
                        new LoggedPipeNode<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateStorageItemHiddenCommentRequestContract>)) as ILogger<IUpdateStorageItemHiddenCommentRequestContract>,
                            new UpdateStorageItemHiddenCommentUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpPatch]
        [Route("PurchasePrice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemPurchasePriceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemPurchasePriceErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemPurchasePrice(
            [FromBody] UpdateStorageItemPurchasePriceHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateStorageItemPurchasePriceHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateStorageItemPurchasePriceHttpBody>)) as IValidator<UpdateStorageItemPurchasePriceHttpBody>, 
                    new ConvertedUpdateStorageItemPurchasePriceOnHttpContext(
                        new LoggedPipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateStorageItemPurchasePriceRequestContract>)) as ILogger<IUpdateStorageItemPurchasePriceRequestContract>,
                            new UpdateStorageItemPurchasePriceUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
        
        [HttpPatch]
        [Route("RetailPrice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemRetailPriceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemRetailPriceErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemRetailPrice(
            [FromBody] UpdateStorageItemRetailPriceHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateStorageItemRetailPriceHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateStorageItemRetailPriceHttpBody>)) as IValidator<UpdateStorageItemRetailPriceHttpBody>, 
                    new ConvertedUpdateStorageItemRetailPriceOnHttpContext(
                        new LoggedPipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateStorageItemRetailPriceRequestContract>)) as ILogger<IUpdateStorageItemRetailPriceRequestContract>,
                            new UpdateStorageItemRetailPriceUseCase(_services.GetService(typeof(IBus)) as IBus))))
                    .Ask(body);
        }
    }
}