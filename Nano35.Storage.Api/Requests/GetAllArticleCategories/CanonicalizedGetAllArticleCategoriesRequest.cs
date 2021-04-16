using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class CanonicalizedGetAllArticleCategoriesRequest : 
        PipeInConvert
        <GetAllArticlesCategoriesHttpQuery, 
            IActionResult,
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract>
    {
        public CanonicalizedGetAllArticleCategoriesRequest(IPipeNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllArticlesCategoriesHttpQuery input)
        {
            var converted = new GetAllArticlesCategoriesRequestContract()
            {
                ParentId = input.ParentId,
                InstanceId = input.InstanceId
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