using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class CanonicalizedGetArticleByIdRequest : 
        PipeInConvert
        <GetArticleByIdHttpQuery, 
            IActionResult,
            IGetArticleByIdRequestContract, 
            IGetArticleByIdResultContract>
    {
        public CanonicalizedGetArticleByIdRequest(IPipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetArticleByIdHttpQuery input)
        {
            var converted = new GetArticleByIdRequestContract()
            {
                Id = input.Id
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetArticleByIdSuccessResultContract success => new OkObjectResult(success),
                IGetArticleByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}