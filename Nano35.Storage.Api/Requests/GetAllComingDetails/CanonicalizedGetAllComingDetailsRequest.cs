using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllComingDetails
{
    public class CanonicalizedGetAllComingDetailsRequest : 
        PipeInConvert
        <GetAllComingDetailsHttpQuery, 
            IActionResult,
            IGetAllComingDetailsRequestContract, 
            IGetAllComingDetailsResultContract>
    {
        public CanonicalizedGetAllComingDetailsRequest(
            IPipeNode<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllComingDetailsHttpQuery input)
        {
            var converted = new GetAllComingDetailsRequestContract()
            {
                ComingId = input.ComingId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllComingDetailsSuccessResultContract success => new OkObjectResult(success),
                IGetAllComingDetailsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}