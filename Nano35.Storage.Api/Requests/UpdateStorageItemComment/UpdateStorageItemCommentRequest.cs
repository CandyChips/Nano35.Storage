using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentRequest :
        MasstransitRequest
        <IUpdateStorageItemCommentRequestContract,
            IUpdateStorageItemCommentResultContract,
            IUpdateStorageItemCommentSuccessResultContract,
            IUpdateStorageItemCommentErrorResultContract>
    {
        public UpdateStorageItemCommentRequest(IBus bus) : base(bus)
        {
        }
    }
}