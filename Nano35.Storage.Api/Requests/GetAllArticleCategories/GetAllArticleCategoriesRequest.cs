using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class GetAllArticleCategoriesRequest:
        MasstransitRequest<
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract,
            IGetAllArticlesCategoriesSuccessResultContract,
            IGetAllArticlesCategoriesErrorResultContract>
    {
        public GetAllArticleCategoriesRequest(IBus bus) : base(bus) {}
    }
}