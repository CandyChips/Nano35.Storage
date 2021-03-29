using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseOfStorageItem
{
    public class ConvertedGetAllWarehouseOfStorageItemOnHttpContext : 
        PipeInConvert
        <GetAllWarehouseOfStorageItemHttpQuery, 
            IActionResult,
            IGetAllWarehouseOfStorageItemRequestContract, 
            IGetAllWarehouseOfStorageItemResultContract>
    {
        public ConvertedGetAllWarehouseOfStorageItemOnHttpContext(
            IPipeNode<IGetAllWarehouseOfStorageItemRequestContract, IGetAllWarehouseOfStorageItemResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllWarehouseOfStorageItemHttpQuery input)
        {
            var converted = new GetAllWarehouseOfStorageItemRequestContract()
            {
                StorageItemId = input.StorageItemId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllWarehouseOfStorageItemSuccessResultContract success => new OkObjectResult(success),
                IGetAllWarehouseOfStorageItemErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}