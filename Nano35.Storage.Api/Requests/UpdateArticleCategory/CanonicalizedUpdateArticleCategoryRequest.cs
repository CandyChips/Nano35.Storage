using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleCategory
{
    public class CanonicalizedUpdateArticleCategoryRequest : 
        PipeInConvert
        <UpdateArticleCategoryHttpBody, 
            IActionResult,
            IUpdateArticleCategoryRequestContract, 
            IUpdateArticleCategoryResultContract>
    {
        public CanonicalizedUpdateArticleCategoryRequest(
            IPipeNode<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateArticleCategoryHttpBody input)
        {
            var converted = new UpdateArticleCategoryRequestContract()
            {
                Id = input.Id,
                CategoryId = input.CategoryId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateArticleCategorySuccessResultContract success => new OkObjectResult(success),
                IUpdateArticleCategoryErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}