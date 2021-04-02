using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class CanonicalizedGetAllArticleModelsRequest : 
        PipeInConvert
        <GetAllArticleModelsHttpQuery, 
            IActionResult,
            IGetAllArticlesModelsRequestContract, 
            IGetAllArticlesModelsResultContract>
    {
        public CanonicalizedGetAllArticleModelsRequest(IPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllArticleModelsHttpQuery input)
        {
            var converted = new GetAllArticlesModelsRequestContract()
            {
                CategoryId = input.CategoryId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllArticlesModelsSuccessResultContract success => new OkObjectResult(success),
                IGetAllArticlesModelsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}