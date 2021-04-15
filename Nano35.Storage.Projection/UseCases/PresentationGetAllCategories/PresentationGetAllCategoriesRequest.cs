using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllCategories
{
    public class PresentationGetAllCategoriesRequest :
        MasstransitRequest
        <IPresentationGetAllCategoriesRequestContract,
            IPresentationGetAllCategoriesResultContract,
            IPresentationGetAllCategoriesSuccessResultContract,
            IPresentationGetAllCategoriesErrorResultContract>
    {
        public PresentationGetAllCategoriesRequest(IBus bus) : base(bus) {}
    }
}