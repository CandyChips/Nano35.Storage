using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllMoves
{
    public class ConvertedGetAllMovesOnHttpContext : 
        PipeInConvert
        <GetAllMovesHttpQuery, 
            IActionResult,
            IGetAllMovesRequestContract, 
            IGetAllMovesResultContract>
    {
        public ConvertedGetAllMovesOnHttpContext(
            IPipeNode<IGetAllMovesRequestContract, IGetAllMovesResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllMovesHttpQuery input)
        {
            var converted = new GetAllMovesRequestContract()
            {
                InstanceId = input.InstanceId,
                StorageItemId = input.StorageItemId,
                UnitId = input.UnitId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllMovesSuccessResultContract success => new OkObjectResult(success),
                IGetAllMovesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}