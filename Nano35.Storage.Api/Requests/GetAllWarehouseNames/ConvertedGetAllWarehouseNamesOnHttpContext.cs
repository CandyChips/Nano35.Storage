using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseNames
{
    public class ConvertedGetAllWarehouseNamesOnHttpContext : 
        PipeInConvert
        <GetAllWarehouseNamesHttpQuery, 
            IActionResult,
            IGetAllWarehouseNamesRequestContract, 
            IGetAllWarehouseNamesResultContract>
    {
        public ConvertedGetAllWarehouseNamesOnHttpContext(
            IPipeNode<IGetAllWarehouseNamesRequestContract, IGetAllWarehouseNamesResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllWarehouseNamesHttpQuery input)
        {
            var converted = new GetAllWarehouseNamesRequestContract()
            {
                UnitId = input.UnitId,
                StorageItemId = input.StorageItemId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllWarehouseNamesSuccessResultContract success => new OkObjectResult(success),
                IGetAllWarehouseNamesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}