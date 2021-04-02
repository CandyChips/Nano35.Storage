using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.Storage.Api.Requests.UpdateArticleBrand
{
    public class CanonicalizedUpdateArticleBrandRequest : 
        PipeInConvert
        <UpdateArticleBrandHttpBody, 
            IActionResult,
            IUpdateArticleBrandRequestContract, 
            IUpdateArticleBrandResultContract>
    {
        public CanonicalizedUpdateArticleBrandRequest(
            IPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateArticleBrandHttpBody input)
        {
            var converted = new UpdateArticleBrandRequestContract()
            {
                Id = input.Id,
                Brand = input.Brand
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateArticleBrandSuccessResultContract success => new OkObjectResult(success),
                IUpdateArticleBrandErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}