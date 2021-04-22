using System;
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
        <Guid, 
            IActionResult,
            IGetArticleByIdRequestContract, 
            IGetArticleByIdResultContract>
    {
        public CanonicalizedGetArticleByIdRequest(IPipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(Guid id)
        {
            var converted = new GetArticleByIdRequestContract()
            {
                Id = id
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