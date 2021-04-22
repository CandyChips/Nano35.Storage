using System;
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
    public class CanonicalizedGetStorageItemByIdRequest : 
        PipeInConvert
        <Guid, 
            IActionResult,
            IGetStorageItemByIdRequestContract, 
            IGetStorageItemByIdResultContract>
    {
        public CanonicalizedGetStorageItemByIdRequest(
            IPipeNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(Guid id)
        {
            var converted = new GetStorageItemByIdRequestContract()
            {
                Id = id
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