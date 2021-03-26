using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleRequest :
        MasstransitRequest
        <IUpdateStorageItemArticleRequestContract,
            IUpdateStorageItemArticleResultContract,
            IUpdateStorageItemArticleSuccessResultContract,
            IUpdateStorageItemArticleErrorResultContract>
    {
        public UpdateStorageItemArticleRequest(IBus bus) : base(bus) {}
    }
}