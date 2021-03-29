using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class GetAllArticlesRequest :
        MasstransitRequest<
            IGetAllArticlesRequestContract,
            IGetAllArticlesResultContract,
            IGetAllArticlesSuccessResultContract,
            IGetAllArticlesErrorResultContract>
    {
        public GetAllArticlesRequest(IBus bus) : base(bus) {}
    }
}