using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllMoveDetails
{
    public class CanonicalizedGetAllMoveDetailsRequest : 
        PipeInConvert
        <GetAllMoveDetailsHttpQuery, 
            IActionResult,
            IGetAllMoveDetailsRequestContract, 
            IGetAllMoveDetailsResultContract>
    {
        public CanonicalizedGetAllMoveDetailsRequest(
            IPipeNode<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllMoveDetailsHttpQuery input)
        {
            var converted = new GetAllMoveDetailsRequestContract()
            {
                MoveId = input.MoveId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllMoveDetailsSuccessResultContract success => new OkObjectResult(success),
                IGetAllMoveDetailsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}