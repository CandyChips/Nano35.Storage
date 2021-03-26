using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentRequest :
        MasstransitRequest
        <IUpdateStorageItemHiddenCommentRequestContract,
            IUpdateStorageItemHiddenCommentResultContract,
            IUpdateStorageItemHiddenCommentSuccessResultContract,
            IUpdateStorageItemHiddenCommentErrorResultContract>
    {
        public UpdateStorageItemHiddenCommentRequest(IBus bus) : base(bus) {}
    }
}