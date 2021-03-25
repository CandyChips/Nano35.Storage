using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice
{
    public class ConvertedUpdateStorageItemPurchasePriceOnHttpContext : 
        PipeInConvert
        <UpdateStorageItemPurchasePriceHttpBody, 
            IActionResult,
            IUpdateStorageItemPurchasePriceRequestContract, 
            IUpdateStorageItemPurchasePriceResultContract>
    {
        public ConvertedUpdateStorageItemPurchasePriceOnHttpContext(
            IPipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateStorageItemPurchasePriceHttpBody input)
        {
            var converted = new UpdateStorageItemPurchasePriceRequestContract()
            {
                Id = input.Id,
                PurchasePrice = input.PurchasePrice
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateStorageItemPurchasePriceSuccessResultContract success => new OkObjectResult(success),
                IUpdateStorageItemPurchasePriceErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}