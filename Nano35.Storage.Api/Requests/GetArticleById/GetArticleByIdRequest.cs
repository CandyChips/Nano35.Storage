using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdRequest :
        MasstransitRequest
        <IGetArticleByIdRequestContract,
            IGetArticleByIdResultContract,
            IGetArticleByIdSuccessResultContract,
            IGetArticleByIdErrorResultContract>
    {
        public GetArticleByIdRequest(IBus bus) : base(bus) {}
    }
}