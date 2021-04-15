using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.storage;

namespace Nano35.Storage.Projection.UseCases.GetAllStorageItemConditions
{
    public class CanonicalizedGetAllStorageItemConditionsRequest : 
        PipeInConvert
        <GetAllStorageItemConditionsHttpQuery, 
            IActionResult,
            IGetAllStorageItemConditionsRequestContract, 
            IGetAllStorageItemConditionsResultContract>
    {
        public CanonicalizedGetAllStorageItemConditionsRequest(
            IPipeNode<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllStorageItemConditionsHttpQuery input)
        {
            var converted = new GetAllStorageItemConditionsRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllStorageItemConditionsSuccessResultContract success => new OkObjectResult(success),
                IGetAllStorageItemConditionsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}