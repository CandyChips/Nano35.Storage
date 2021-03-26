using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class GetAllArticleBrandsRequest:
        MasstransitRequest<
            IGetAllArticlesBrandsRequestContract,
            IGetAllArticlesBrandsResultContract,
            IGetAllArticlesBrandsSuccessResultContract,
            IGetAllArticlesBrandsErrorResultContract>
    {
        public GetAllArticleBrandsRequest(IBus bus) : base(bus) {}
    }
}