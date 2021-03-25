using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class ConvertedUpdateArticleInfoOnHttpContext : 
        PipeInConvert
        <UpdateArticleInfoHttpBody, 
            IActionResult,
            IUpdateArticleInfoRequestContract, 
            IUpdateArticleInfoResultContract>
    {
        public ConvertedUpdateArticleInfoOnHttpContext(
            IPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateArticleInfoHttpBody input)
        {
            var converted = new UpdateArticleInfoRequestContract()
            {
                Id = input.Id,
                Info = input.Info
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateArticleInfoSuccessResultContract success => new OkObjectResult(success),
                IUpdateArticleInfoErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}