using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnUnit
{
    public class ConvertedGetAllPlacesOfStorageItemOnUnitOnHttpContext : 
        PipeInConvert
        <GetAllPlacesOfStorageItemOnUnitHttpQuery, 
            IActionResult,
            IGetAllPlacesOfStorageItemOnUnitRequestContract, 
            IGetAllPlacesOfStorageItemOnUnitResultContract>
    {
        public ConvertedGetAllPlacesOfStorageItemOnUnitOnHttpContext(
            IPipeNode<IGetAllPlacesOfStorageItemOnUnitRequestContract, IGetAllPlacesOfStorageItemOnUnitResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllPlacesOfStorageItemOnUnitHttpQuery input)
        {
            var converted = new GetAllPlacesOfStorageItemOnUnitRequestContract()
            {
                StorageItemId = input.StorageItemId,
                UnitContainsStorageItemId = input.UnitContainsStorageItemId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllPlacesOfStorageItemOnUnitSuccessResultContract success => new OkObjectResult(success),
                IGetAllPlacesOfStorageItemOnUnitErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}