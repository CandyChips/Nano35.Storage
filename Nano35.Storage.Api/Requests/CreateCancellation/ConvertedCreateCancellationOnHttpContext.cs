using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;
using CreateCancellationDetailViewModel = Nano35.Contracts.Storage.Models.CreateCancellationDetailViewModel;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class ConvertedCreateCancellationOnHttpContext : 
        PipeInConvert
        <CreateCancellationHttpBody,
            IActionResult,
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract>
    {
        public ConvertedCreateCancellationOnHttpContext(
            IPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(CreateCancellationHttpBody input)
        {
            var converted = new CreateCancellationRequestContract()
            {
                Comment = input.Comment,
                InstanceId = input.InstanceId,
                NewId = input.NewId,
                Number = input.Number,
                UnitId = input.UnitId,
                Details = input.Details.Select(a => new CreateCancellationDetailViewModel()
                {
                    NewId = a.NewId,
                    Count = a.Count,
                    PlaceOnStorage = a.PlaceOnStorage,
                    StorageItemId = a.StorageItemId
                }).ToList()
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateCancellationSuccessResultContract success => new OkObjectResult(success),
                ICreateCancellationErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}