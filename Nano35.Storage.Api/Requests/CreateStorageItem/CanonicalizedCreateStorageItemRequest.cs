using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CanonicalizedCreateStorageItemRequest : 
        PipeInConvert
        <CreateStorageItemHttpBody, 
            IActionResult,
            ICreateStorageItemRequestContract, 
            ICreateStorageItemResultContract>
    {
        public CanonicalizedCreateStorageItemRequest(IPipeNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(CreateStorageItemHttpBody input)
        {
            var converted = new CreateStorageItemRequestContract()
            {
                ArticleId = input.ArticleId,
                Comment = input.Comment ?? "",
                ConditionId = input.ConditionId,
                HiddenComment = input.HiddenComment ?? "",
                InstanceId = input.InstanceId,
                NewId = input.NewId,
                PurchasePrice = input.PurchasePrice,
                RetailPrice = input.RetailPrice
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateStorageItemSuccessResultContract success => new OkObjectResult(success),
                ICreateStorageItemErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}