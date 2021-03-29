using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class GetAllStorageItemsRequest :
        MasstransitRequest
        <IGetAllStorageItemsRequestContract,
            IGetAllStorageItemsResultContract,
            IGetAllStorageItemsSuccessResultContract,
            IGetAllStorageItemsErrorResultContract>
    {
        public GetAllStorageItemsRequest(IBus bus) : base(bus) {}
    }
}