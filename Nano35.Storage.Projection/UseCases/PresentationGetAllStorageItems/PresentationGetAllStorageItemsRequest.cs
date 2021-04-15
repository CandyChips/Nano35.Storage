using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllStorageItems
{
    public class PresentationGetAllStorageItemsRequest :
        MasstransitRequest
        <IPresentationGetAllStorageItemsRequestContract,
            IPresentationGetAllStorageItemsResultContract,
            IPresentationGetAllStorageItemsSuccessResultContract,
            IPresentationGetAllStorageItemsErrorResultContract>
    {
        public PresentationGetAllStorageItemsRequest(IBus bus) : base(bus) {}
    }
}