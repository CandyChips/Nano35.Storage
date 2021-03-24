using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOnStorage
{
    public class ConvertedGetAllPlacesOnStorageOnHttpContext : 
        PipeInConvert
        <GetAllPlacesOnStorageHttpContext, 
            IActionResult,
            IGetAllPlacesOnStorageContract, 
            IGetAllPlacesOnStorageResultContract>
    {
        public ConvertedGetAllPlacesOnStorageOnHttpContext(
            IPipeNode<IGetAllPlacesOnStorageContract, IGetAllPlacesOnStorageResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(GetAllPlacesOnStorageHttpContext input)
        {
            var converted = new GetAllPlacesOnStorageContract()
            {
                UnitId = input.UnitId != Guid.Empty ? input.UnitId : Guid.Empty,
                StorageItemId = input.StorageItemId != Guid.Empty ? input.StorageItemId : Guid.Empty,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllPlacesOnStorageSuccessResultContract success => new OkObjectResult(success),
                IGetAllPlacesOnStorageErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}