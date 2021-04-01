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
    public class ConvertedGetAllStorageItemsOnInstanceOnHttpContext : 
        PipeInConvert
        <GetAllStorageItemsOnInstanceHttpQuery, 
            IActionResult,
            IGetAllStorageItemsOnInstanceContract, 
            IGetAllStorageItemsOnInstanceResultContract>
    {
        public ConvertedGetAllStorageItemsOnInstanceOnHttpContext(
            IPipeNode<IGetAllStorageItemsOnInstanceContract, IGetAllStorageItemsOnInstanceResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllStorageItemsOnInstanceHttpQuery input)
        {
            var converted = new GetAllStorageItemsOnInstanceContract()
            {
                InstanceId = input.InstanceId,
                
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllStorageItemsOnInstanceSuccessResultContract success => new OkObjectResult(success),
                IGetAllStorageItemsOnInstanceErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}