using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class CanonicalizedGetAllComingsRequest : 
        PipeInConvert
        <GetAllComingsHttpQuery, 
            IActionResult,
            IGetAllComingsRequestContract, 
            IGetAllComingsResultContract>
    {
        public CanonicalizedGetAllComingsRequest(
            IPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllComingsHttpQuery input)
        {
            var converted = new GetAllComingsRequestContract()
            {
                InstanceId = input.InstanceId,
                StorageItemId = input.StorageItemId,
                UnitId = input.UnitId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllComingsSuccessResultContract success => new OkObjectResult(success),
                IGetAllComingsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}