using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelsRequest :
        MasstransitRequest<
            IGetAllArticlesModelsRequestContract,
            IGetAllArticlesModelsResultContract,
            IGetAllArticlesModelsSuccessResultContract,
            IGetAllArticlesModelsErrorResultContract>
    {
        public GetAllArticlesModelsRequest(IBus bus) : base(bus) {}
    }
}