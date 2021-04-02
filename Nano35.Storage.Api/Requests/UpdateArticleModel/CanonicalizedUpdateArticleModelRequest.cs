using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class CanonicalizedUpdateArticleModelRequest : 
        PipeInConvert
        <UpdateArticleModelHttpBody, 
            IActionResult,
            IUpdateArticleModelRequestContract, 
            IUpdateArticleModelResultContract>
    {
        public CanonicalizedUpdateArticleModelRequest(
            IPipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateArticleModelHttpBody input)
        {
            var converted = new UpdateArticleModelRequestContract()
            {
                Id = input.Id,
                Model = input.Model
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateArticleModelSuccessResultContract success => new OkObjectResult(success),
                IUpdateArticleModelErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}