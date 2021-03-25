using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemConditions
{
    public class ConvertedGetAllStorageItemConditionsOnHttpContext : 
        PipeInConvert
        <GetAllStorageItemConditionsHttpQuery, 
            IActionResult,
            IGetAllStorageItemConditionsRequestContract, 
            IGetAllStorageItemConditionsResultContract>
    {
        public ConvertedGetAllStorageItemConditionsOnHttpContext(
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