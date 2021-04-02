using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemCondition
{
    public class CanonicalizedUpdateStorageItemConditionRequest : 
        PipeInConvert
        <UpdateStorageItemConditionHttpBody, 
            IActionResult,
            IUpdateStorageItemConditionRequestContract, 
            IUpdateStorageItemConditionResultContract>
    {
        public CanonicalizedUpdateStorageItemConditionRequest(
            IPipeNode<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateStorageItemConditionHttpBody input)
        {
            var converted = new UpdateStorageItemConditionRequestContract()
            {
                Id = input.Id,
                ConditionId = input.ConditionId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateStorageItemConditionSuccessResultContract success => new OkObjectResult(success),
                IUpdateStorageItemConditionErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}