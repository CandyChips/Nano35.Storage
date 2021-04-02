using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId
{
    public class CanonicalizedUpdateCategoryParentCategoryIdRequest : 
        PipeInConvert
        <UpdateCategoryParentCategoryHttpBody, 
            IActionResult,
            IUpdateCategoryParentCategoryIdRequestContract, 
            IUpdateCategoryParentCategoryIdResultContract>
    {
        public CanonicalizedUpdateCategoryParentCategoryIdRequest(IPipeNode<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateCategoryParentCategoryHttpBody input)
        {
            var converted = new UpdateCategoryParentCategoryIdRequestContract()
            {
                Id = input.Id,
                ParentCategoryId = input.ParentCategoryId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateCategoryParentCategoryIdSuccessResultContract success => new OkObjectResult(success),
                IUpdateCategoryParentCategoryIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}