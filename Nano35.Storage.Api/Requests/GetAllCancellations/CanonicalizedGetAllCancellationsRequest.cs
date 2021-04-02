using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellations
{
    public class CanonicalizedGetAllCancellationsRequest : 
        PipeInConvert
        <GetAllCancellationsHttpQuery, 
            IActionResult,
            IGetAllCancellationsRequestContract, 
            IGetAllCancellationsResultContract>
    {
        public CanonicalizedGetAllCancellationsRequest(
            IPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllCancellationsHttpQuery input)
        {
            var converted = new GetAllCancellationsRequestContract()
            {
                InstanceId = input.InstanceId,
                StorageItemId = input.StorageItemId,
                UnitId = input.UnitId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllCancellationsSuccessResultContract success => new OkObjectResult(success),
                IGetAllCancellationsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}