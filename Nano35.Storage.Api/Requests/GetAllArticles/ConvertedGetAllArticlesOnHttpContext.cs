using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class ConvertedGetAllArticlesOnHttpContext : 
        PipeInConvert
        <GetAllArticlesHttpQuery, 
            IActionResult,
            IGetAllArticlesRequestContract, 
            IGetAllArticlesResultContract>
    {
        public ConvertedGetAllArticlesOnHttpContext(IPipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllArticlesHttpQuery input)
        {
            var converted = new GetAllArticlesRequestContract()
            {
                InstanceId = input.InstanceId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllArticlesSuccessResultContract success => new OkObjectResult(success),
                IGetAllArticlesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}