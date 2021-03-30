using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceRequest :
        MasstransitRequest
        <IGetAllStorageItemsOnInstanceContract,
            IGetAllStorageItemsOnInstanceResultContract,
            IGetAllStorageItemsOnInstanceSuccessResultContract,
            IGetAllStorageItemsOnInstanceErrorResultContract>
    {
        public GetAllStorageItemsOnInstanceRequest(IBus bus) : base(bus) {}
    }
}