using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateCategoryName
{
    public class CanonicalizedUpdateCategoryNameRequest : 
        PipeInConvert
        <UpdateCategoryNameHttpBody, 
            IActionResult,
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract>
    {
        public CanonicalizedUpdateCategoryNameRequest(IPipeNode<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateCategoryNameHttpBody input)
        {
            var converted = new UpdateCategoryNameRequestContract()
            {
                Id = input.Id,
                Name = input.Name
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateCategoryNameSuccessResultContract success => new OkObjectResult(success),
                IUpdateCategoryNameErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}