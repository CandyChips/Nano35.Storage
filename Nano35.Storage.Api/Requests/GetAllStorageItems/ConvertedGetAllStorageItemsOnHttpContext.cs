using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class ConvertedGetAllStorageItemsOnHttpContext : 
        PipeInConvert
        <GetAllStorageItemsQuery, 
            IActionResult,
            IGetAllStorageItemsRequestContract, 
            IGetAllStorageItemsResultContract>
    {
        public ConvertedGetAllStorageItemsOnHttpContext(
            IPipeNode<IGetAllStorageItemsRequestContract, IGetAllStorageItemsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllStorageItemsQuery input)
        {
            var converted = new GetAllStorageItemsRequestContract()
            {
                InstanceId = input.InstanceId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllStorageItemsSuccessResultContract success => new OkObjectResult(success),
                IGetAllStorageItemsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}