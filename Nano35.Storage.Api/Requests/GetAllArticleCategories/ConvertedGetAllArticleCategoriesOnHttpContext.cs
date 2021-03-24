using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class ConvertedGetAllArticleCategoriesOnHttpContext : 
        PipeInConvert
        <GetAllArticlesCategoriesHttpQuery, 
            IActionResult,
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract>
    {
        public ConvertedGetAllArticleCategoriesOnHttpContext(IPipeNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllArticlesCategoriesHttpQuery input)
        {
            var converted = new GetAllArticlesCategoriesRequestContract()
            {
                InstanceId = input.InstanceId,
                ParentId = input.ParentId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllArticlesCategoriesSuccessResultContract success => new OkObjectResult(success),
                IGetAllArticlesCategoriesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}