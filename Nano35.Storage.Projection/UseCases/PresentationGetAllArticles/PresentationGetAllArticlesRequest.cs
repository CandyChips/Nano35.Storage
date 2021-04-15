using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllArticles
{
    public class PresentationGetAllArticlesRequest :
        MasstransitRequest
        <IPresentationGetAllArticlesRequestContract,
            IPresentationGetAllArticlesResultContract,
            IPresentationGetAllArticlesSuccessResultContract,
            IPresentationGetAllArticlesErrorResultContract>
    {
        public PresentationGetAllArticlesRequest(IBus bus) : base(bus) {}
    }
}