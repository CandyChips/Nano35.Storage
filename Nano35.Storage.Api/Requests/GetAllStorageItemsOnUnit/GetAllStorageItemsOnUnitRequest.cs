using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitRequest :
        MasstransitRequest
        <IGetAllStorageItemsOnUnitContract,
            IGetAllStorageItemsOnUnitResultContract,
            IGetAllStorageItemsOnUnitSuccessResultContract,
            IGetAllStorageItemsOnUnitErrorResultContract>
    {
        public GetAllStorageItemsOnUnitRequest(IBus bus) : base(bus) {}
    }
}