using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllArticles
{
    public class CanonicalizedPresentationGetAllArticlesRequestRequest : 
        PipeInConvert
        <PresentationGetAllArticlesHttpQuery, 
            IActionResult,
            IPresentationGetAllArticlesRequestContract, 
            IPresentationGetAllArticlesResultContract>
    {
        public CanonicalizedPresentationGetAllArticlesRequestRequest(
            IPipeNode<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(PresentationGetAllArticlesHttpQuery input)
        {
            var converted = new PresentationGetAllArticlesRequestContract()
            {
                InstanceId = input.InstanceId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IPresentationGetAllArticlesSuccessResultContract success => new OkObjectResult(success),
                IPresentationGetAllArticlesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}