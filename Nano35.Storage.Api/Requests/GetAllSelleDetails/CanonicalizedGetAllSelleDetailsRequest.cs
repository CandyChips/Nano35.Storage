using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSelleDetails
{
    public class CanonicalizedGetAllSelleDetailsRequest : 
        PipeInConvert
        <GetAllSellDetailsHttpQuery, 
            IActionResult,
            IGetAllSelleDetailsRequestContract, 
            IGetAllSelleDetailsResultContract>
    {
        public CanonicalizedGetAllSelleDetailsRequest(
            IPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllSellDetailsHttpQuery input)
        {
            var converted = new GetAllSelleDetailsRequestContract()
            {
                SelleId = input.SelleId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllSelleDetailsSuccessResultContract success => new OkObjectResult(success),
                IGetAllSelleDetailsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}