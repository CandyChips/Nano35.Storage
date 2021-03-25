using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSells
{
    public class ConvertedGetAllSellsOnHttpContext : 
        PipeInConvert
        <GetAllSellsHttpQuery, 
            IActionResult,
            IGetAllSellsRequestContract, 
            IGetAllSellsResultContract>
    {
        public ConvertedGetAllSellsOnHttpContext(
            IPipeNode<IGetAllSellsRequestContract, IGetAllSellsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllSellsHttpQuery input)
        {
            var converted = new GetAllSellsRequestContract()
            {
                InstanceId = input.InstanceId,
                UnitId = input.UnitId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllSellsSuccessResultContract success => new OkObjectResult(success),
                IGetAllSellsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}