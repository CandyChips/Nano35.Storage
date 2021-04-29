using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;
using CreateComingDetailViewModel = Nano35.Contracts.Storage.Models.CreateComingDetailViewModel;

namespace Nano35.Storage.Api.Requests.CreateComing
{
    public class CanonicalizedCreateComingRequest : 
        PipeInConvert
        <CreateComingHttpBody, 
            IActionResult,
            ICreateComingRequestContract, 
            ICreateComingResultContract>
    {
        public CanonicalizedCreateComingRequest(IPipeNode<ICreateComingRequestContract, ICreateComingResultContract> next) : base(next) {}
        public override async Task<IActionResult> Ask(CreateComingHttpBody input)
        {
            var converted = new CreateComingRequestContract()
                                {NewId = input.NewId,
                                 InstanceId = input.InstanceId,
                                 Number = input.Number ?? "",
                                 UnitId = input.UnitId,
                                 Comment = input.Comment ?? "",
                                 Details = input
                                     .Details
                                     .Select(a => 
                                         new CreateComingDetailViewModel()
                                             {Count = a.Count,
                                              Price = a.Price,
                                              NewId = a.NewId,
                                              StorageItemId = a.StorageItemId,
                                              PlaceOnStorage = a.PlaceOnStorage})
                                     .ToList(),
                                 ClientId = input.ClientId};
            return await DoNext(converted) switch
            {
                ICreateComingSuccessResultContract success => new OkObjectResult(success),
                ICreateComingErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}