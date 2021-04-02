using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice
{
    public class CanonicalizedUpdateStorageItemRetailPriceRequest : 
        PipeInConvert
        <UpdateStorageItemRetailPriceHttpBody, 
            IActionResult,
            IUpdateStorageItemRetailPriceRequestContract, 
            IUpdateStorageItemRetailPriceResultContract>
    {
        public CanonicalizedUpdateStorageItemRetailPriceRequest(
            IPipeNode<IUpdateStorageItemRetailPriceRequestContract, IUpdateStorageItemRetailPriceResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateStorageItemRetailPriceHttpBody input)
        {
            var converted = new UpdateStorageItemRetailPriceRequestContract()
            {
                Id = input.Id,
                RetailPrice = input.RetailPrice
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateStorageItemRetailPriceSuccessResultContract success => new OkObjectResult(success),
                IUpdateStorageItemRetailPriceErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}