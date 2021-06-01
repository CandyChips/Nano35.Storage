using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;
using Nano35.Storage.Api.Requests;
using Nano35.Storage.Api.Requests.CheckExistArticle;
using Nano35.Storage.Api.Requests.CheckExistStorageItem;
using Nano35.Storage.Api.Requests.CreateStorageItem;
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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllStorageItems(
            [FromQuery] GetAllStorageItemsQuery header) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllStorageItemsRequestContract>)) as ILogger<IGetAllStorageItemsRequestContract>,
                        new GetAllStorageItemsUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetAllStorageItemsRequestContract()
                    {
                        InstanceId = header.InstanceId
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetStorageItemByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetStorageItemByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetStorageItemById(Guid id) 
        {
            var result =
                await new LoggedUseCasePipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>(
                        _services.GetService(typeof(ILogger<IGetStorageItemByIdRequestContract>)) as ILogger<IGetStorageItemByIdRequestContract>,
                        new GetStorageItemByIdUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new GetStorageItemByIdRequestContract()
                    {
                        Id = id
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateStorageItemSuccessHttpResponse))]
        public async Task<IActionResult> CreateStorageItem(
            [FromBody] CreateStorageItemHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(
                        _services.GetService(typeof(ILogger<ICreateStorageItemRequestContract>)) as ILogger<ICreateStorageItemRequestContract>,
                        new CreateStorageItemUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new CreateStorageItemRequestContract()
                    {
                        NewId = body.NewId,
                        InstanceId = body.InstanceId,
                        Comment = body.Comment ?? "",
                        HiddenComment = body.HiddenComment ?? "",
                        RetailPrice = body.RetailPrice,
                        PurchasePrice = body.PurchasePrice,
                        ArticleId = body.ArticleId,
                        ConditionId = body.ConditionId
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPatch]
        [Route("Article")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemArticleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemArticleErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemArticle(
            [FromBody] UpdateStorageItemArticleHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemArticleRequestContract>)) as
                            ILogger<IUpdateStorageItemArticleRequestContract>,
                        new UpdateStorageItemArticleUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateStorageItemArticleRequestContract() {Id = body.Id, ArticleId = body.ArticleId});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPatch]
        [Route("Comment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemCommentErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemComment(
            [FromBody] UpdateStorageItemCommentHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemCommentRequestContract>)) as
                            ILogger<IUpdateStorageItemCommentRequestContract>,
                        new UpdateStorageItemCommentUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateStorageItemCommentRequestContract() {Id = body.Id, Comment = body.Comment});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPatch]
        [Route("Condition")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemConditionSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemConditionErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemCondition(
            [FromBody] UpdateStorageItemConditionHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemConditionRequestContract>)) as
                            ILogger<IUpdateStorageItemConditionRequestContract>,
                        new UpdateStorageItemConditionUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateStorageItemConditionRequestContract() {Id = body.Id, ConditionId = body.ConditionId});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch]
        [Route("HiddenComment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemHiddenCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemHiddenCommentErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemHiddenComment(
            [FromBody] UpdateStorageItemHiddenCommentHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemHiddenCommentRequestContract>)) as
                            ILogger<IUpdateStorageItemHiddenCommentRequestContract>,
                        new UpdateStorageItemHiddenCommentUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateStorageItemHiddenCommentRequestContract() {Id = body.Id, HiddenComment = body.HiddenComment});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPatch]
        [Route("PurchasePrice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemPurchasePriceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemPurchasePriceErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemPurchasePrice(
            [FromBody] UpdateStorageItemPurchasePriceHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemPurchasePriceRequestContract>)) as
                            ILogger<IUpdateStorageItemPurchasePriceRequestContract>,
                        new UpdateStorageItemPurchasePriceUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateStorageItemPurchasePriceRequestContract() {Id = body.Id, PurchasePrice = body.PurchasePrice});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpPatch]
        [Route("RetailPrice")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateStorageItemRetailPriceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateStorageItemRetailPriceErrorHttpResponse))] 
        public async Task<IActionResult> UpdateStorageItemRetailPrice(
            [FromBody] UpdateStorageItemRetailPriceHttpBody body) 
        {
            var result =
                await new LoggedUseCasePipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateStorageItemRetailPriceRequestContract>)) as
                            ILogger<IUpdateStorageItemRetailPriceRequestContract>,
                        new UpdateStorageItemRetailPriceUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateStorageItemRetailPriceRequestContract() {Id = body.Id, RetailPrice = body.RetailPrice});
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet]
        [Route("ExistStorageItem")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllStorageItemsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllStorageItemsErrorHttpResponse))] 
        public async Task<IActionResult> CheckExistStorageItem(Guid id) 
        {
            var result =
                await new LoggedUseCasePipeNode<ICheckExistStorageItemRequestContract, ICheckExistStorageItemResultContract>(
                        _services.GetService(typeof(ILogger<ICheckExistStorageItemRequestContract>)) as ILogger<ICheckExistStorageItemRequestContract>,
                        new CheckExistStorageItemUseCase(
                            _services.GetService((typeof(IBus))) as IBus))
                    .Ask(new CheckExistStorageItemRequestContract()
                    {
                        StorageItemId = id
                    });
            
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
    }
}