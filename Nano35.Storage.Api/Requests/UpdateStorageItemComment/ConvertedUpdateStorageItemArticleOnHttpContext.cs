using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemComment
{
    public class CanonicalizedUpdateStorageItemCommentRequest : 
        PipeInConvert
        <UpdateStorageItemCommentHttpBody, 
            IActionResult,
            IUpdateStorageItemCommentRequestContract, 
            IUpdateStorageItemCommentResultContract>
    {
        public CanonicalizedUpdateStorageItemCommentRequest(
            IPipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateStorageItemCommentHttpBody input)
        {
            var converted = new UpdateStorageItemCommentRequestContract()
            {
                Id = input.Id,
                Comment = input.Comment ?? ""
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateStorageItemCommentSuccessResultContract success => new OkObjectResult(success),
                IUpdateStorageItemCommentErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}