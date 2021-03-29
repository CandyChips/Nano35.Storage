using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class UpdateArticleInfoRequest : 
        MasstransitRequest
        <IUpdateArticleInfoRequestContract,
            IUpdateArticleInfoResultContract,
            IUpdateArticleInfoSuccessResultContract,
            IUpdateArticleInfoErrorResultContract>
    {
        public UpdateArticleInfoRequest(IBus bus) : base(bus) {}
    }
}