using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.storage;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment
{
    public class CanonicalizedUpdateStorageItemHiddenCommentRequest : 
        PipeInConvert
        <UpdateStorageItemHiddenCommentHttpBody, 
            IActionResult,
            IUpdateStorageItemHiddenCommentRequestContract, 
            IUpdateStorageItemHiddenCommentResultContract>
    {
        public CanonicalizedUpdateStorageItemHiddenCommentRequest(
            IPipeNode<IUpdateStorageItemHiddenCommentRequestContract, IUpdateStorageItemHiddenCommentResultContract> next) :
            base(next) {}

        public override async Task<IActionResult> Ask(UpdateStorageItemHiddenCommentHttpBody input)
        {
            var converted = new UpdateStorageItemHiddenCommentRequestContract()
            {
                Id = input.Id,
                HiddenComment = input.HiddenComment ?? ""
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateStorageItemHiddenCommentSuccessResultContract success => new OkObjectResult(success),
                IUpdateStorageItemHiddenCommentErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}