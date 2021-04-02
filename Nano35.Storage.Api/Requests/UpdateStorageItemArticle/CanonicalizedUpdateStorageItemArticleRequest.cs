using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemArticle
{
    public class CanonicalizedUpdateStorageItemArticleRequest : 
        PipeInConvert
        <UpdateStorageItemArticleHttpBody, 
            IActionResult,
            IUpdateStorageItemArticleRequestContract, 
            IUpdateStorageItemArticleResultContract>
    {
        public CanonicalizedUpdateStorageItemArticleRequest(
            IPipeNode<IUpdateStorageItemArticleRequestContract, IUpdateStorageItemArticleResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateStorageItemArticleHttpBody input)
        {
            var converted = new UpdateStorageItemArticleRequestContract()
            {
                Id = input.Id,
                ArticleId = input.ArticleId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateStorageItemArticleSuccessResultContract success => new OkObjectResult(success),
                IUpdateStorageItemArticleErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}