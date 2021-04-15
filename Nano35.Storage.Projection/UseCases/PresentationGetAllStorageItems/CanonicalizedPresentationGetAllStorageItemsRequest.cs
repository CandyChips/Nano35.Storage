using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllStorageItems
{
    public class CanonicalizedPresentationGetAllStorageItemsRequest : 
        PipeInConvert
        <PresentationGetAllStorageItemsHttpQuery, 
            IActionResult,
            IPresentationGetAllStorageItemsRequestContract, 
            IPresentationGetAllStorageItemsResultContract>
    {
        public CanonicalizedPresentationGetAllStorageItemsRequest(
            IPipeNode<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(PresentationGetAllStorageItemsHttpQuery input)
        {
            var converted = new PresentationGetAllStorageItemsRequestContract()
            {
                InstanceId = input.InstanceId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IPresentationGetAllStorageItemsSuccessResultContract success => new OkObjectResult(success),
                IPresentationGetAllStorageItemsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}