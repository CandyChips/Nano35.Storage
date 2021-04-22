using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;
using CreateMoveDetailViewModel = Nano35.Contracts.Storage.Models.CreateMoveDetailViewModel;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CanonicalizedCreateMoveRequest : 
        PipeInConvert
        <CreateMoveHttpBody, 
            IActionResult,
            ICreateMoveRequestContract, 
            ICreateMoveResultContract>
    {
        public CanonicalizedCreateMoveRequest(IPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(CreateMoveHttpBody input)
        {
            var converted = new CreateMoveRequestContract()
            {
                FromUnitId = input.FromUnitId,
                InstanceId = input.InstanceId,
                NewId = input.NewId,
                Number = input.Number ?? "",
                ToUnitId = input.ToUnitId,
                Details = input.Details.Select(a => new CreateMoveDetailViewModel()
                {
                    NewId = a.NewId,
                    Count = a.Count,
                    FromPlaceOnStorage = a.FromPlaceOnStorage,
                    ToPlaceOnStorage = a.ToPlaceOnStorage,
                    StorageItemId = a.StorageItemId
                }).ToList()
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateMoveSuccessResultContract success => new OkObjectResult(success),
                ICreateMoveErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}