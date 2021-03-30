using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnInstance
{
    public class ConvertedGetAllStorageItemsOnUnitOnHttpContext : 
        PipeInConvert
        <GetAllStorageItemsOnUnitHttpQuery, 
            IActionResult,
            IGetAllStorageItemsOnUnitContract, 
            IGetAllStorageItemsOnUnitResultContract>
    {
        public ConvertedGetAllStorageItemsOnUnitOnHttpContext(
            IPipeNode<IGetAllStorageItemsOnUnitContract, IGetAllStorageItemsOnUnitResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllStorageItemsOnUnitHttpQuery input)
        {
            var converted = new GetAllStorageItemsOnUnitContract()
            {
                UnitId = input.UnitId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllStorageItemsOnUnitSuccessResultContract success => new OkObjectResult(success),
                IGetAllStorageItemsOnUnitErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}