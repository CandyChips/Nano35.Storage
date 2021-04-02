using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnInstance
{
    public class CanonicalizedGetAllPlacesOfStorageItemOnInstanceRequest : 
        PipeInConvert
        <GetAllPlacesOfStorageItemOnInstanceHttpQuery, 
            IActionResult,
            IGetAllPlacesOfStorageItemOnInstanceContract, 
            IGetAllPlacesOfStorageItemOnInstanceResultContract>
    {
        public CanonicalizedGetAllPlacesOfStorageItemOnInstanceRequest(
            IPipeNode<IGetAllPlacesOfStorageItemOnInstanceContract, IGetAllPlacesOfStorageItemOnInstanceResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllPlacesOfStorageItemOnInstanceHttpQuery input)
        {
            var converted = new GetAllPlacesOfStorageItemOnInstanceContract()
            {
                StorageItemId = input.StorageItemId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllPlacesOfStorageItemOnInstanceSuccessResultContract success => new OkObjectResult(success),
                IGetAllPlacesOfStorageItemOnInstanceErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}