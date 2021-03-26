using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class UpdateArticleModelRequest :
        MasstransitRequest
        <IUpdateArticleModelRequestContract,
            IUpdateArticleModelResultContract,
            IUpdateArticleModelSuccessResultContract,
            IUpdateArticleModelErrorResultContract>
    {
        public UpdateArticleModelRequest(IBus bus) : base(bus) {}
    }
}