using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class CanonicalizedGetAllCancellationDetailsRequest : 
        PipeInConvert
        <GetAllCancellationDetailsHttpQuery, 
            IActionResult,
            IGetAllCancellationDetailsRequestContract, 
            IGetAllCancellationDetailsResultContract>
    {
        public CanonicalizedGetAllCancellationDetailsRequest(
            LoggedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllCancellationDetailsHttpQuery input)
        {
            var converted = new GetAllCancellationDetailsRequestContract()
            {
                CancellationId = input.CancellationId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllCancellationDetailsSuccessResultContract success => new OkObjectResult(success),
                IGetAllCancellationDetailsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}