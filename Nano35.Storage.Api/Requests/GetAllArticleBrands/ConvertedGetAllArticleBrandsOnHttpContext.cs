using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class ConvertedGetAllArticleBrandsOnHttpContext : 
        PipeInConvert
        <GetAllArticlesBrandsHttpQuery, 
            IActionResult,
            IGetAllArticlesBrandsRequestContract, 
            IGetAllArticlesBrandsResultContract>
    {
        public ConvertedGetAllArticleBrandsOnHttpContext(IPipeNode<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllArticlesBrandsHttpQuery input)
        {
            var converted = new GetAllArticlesBrandsRequestContract()
            {
                CategoryId = input.CategoryId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllArticlesBrandsSuccessResultContract success => new OkObjectResult(success),
                IGetAllArticlesBrandsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}