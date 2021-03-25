using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class ConvertedGetStorageItemByIdOnHttpContext : 
        PipeInConvert
        <GetStorageItemByIdHttpQuery, 
            IActionResult,
            IGetStorageItemByIdRequestContract, 
            IGetStorageItemByIdResultContract>
    {
        public ConvertedGetStorageItemByIdOnHttpContext(
            IPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetStorageItemByIdHttpQuery input)
        {
            var converted = new GetStorageItemByIdRequestContract()
            {
                Id = input.Id
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetStorageItemByIdSuccessResultContract success => new OkObjectResult(success),
                IGetStorageItemByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}