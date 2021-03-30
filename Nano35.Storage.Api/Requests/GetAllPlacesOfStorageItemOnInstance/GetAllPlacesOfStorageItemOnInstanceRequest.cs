using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnInstance
{
    public class GetAllPlacesOfStorageItemOnInstanceRequest :
        MasstransitRequest
        <IGetAllPlacesOfStorageItemOnInstanceContract,
            IGetAllPlacesOfStorageItemOnInstanceResultContract,
            IGetAllPlacesOfStorageItemOnInstanceSuccessResultContract,
            IGetAllPlacesOfStorageItemOnInstanceErrorResultContract>
    {
        public GetAllPlacesOfStorageItemOnInstanceRequest(IBus bus) : base(bus) {}
    }
}