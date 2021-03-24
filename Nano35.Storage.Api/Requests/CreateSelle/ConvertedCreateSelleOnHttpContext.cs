using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;
using CreateSelleDetailViewModel = Nano35.Contracts.Storage.Models.CreateSelleDetailViewModel;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class ConvertedCreateSelleOnHttpContext : 
        PipeInConvert
        <CreateSelleHttpBody, 
            IActionResult,
            ICreateSelleRequestContract, 
            ICreateSelleResultContract>
    {
        public ConvertedCreateSelleOnHttpContext(IPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(CreateSelleHttpBody input)
        {
            var converted = new CreateSelleRequestContract()
            {
                Details = input.Details.Select(a => new CreateSelleDetailViewModel()
                { 
                    NewId = a.NewId, 
                    Count = a.Count, 
                    Price = a.Price,
                    PlaceOnStorage = a.PlaceOnStorage, 
                    StorageItemId = a.StorageItemId
                }).ToList(),
                UnitId = input.UnitId,
                InstanceId = input.InstanceId,
                NewId = input.NewId,
                Number = input.Number,
                ClientId = input.ClientId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateSelleSuccessResultContract success => new OkObjectResult(success),
                ICreateSelleErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}