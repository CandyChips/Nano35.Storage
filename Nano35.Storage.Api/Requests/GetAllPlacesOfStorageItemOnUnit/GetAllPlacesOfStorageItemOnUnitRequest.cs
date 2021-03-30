using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOfStorageItemOnUnit
{
    public class GetAllPlacesOfStorageItemOnUnitRequest :
        MasstransitRequest
        <IGetAllPlacesOfStorageItemOnUnitRequestContract,
            IGetAllPlacesOfStorageItemOnUnitResultContract,
            IGetAllPlacesOfStorageItemOnUnitSuccessResultContract,
            IGetAllPlacesOfStorageItemOnUnitErrorResultContract>
    {
        public GetAllPlacesOfStorageItemOnUnitRequest(IBus bus) : base(bus) {}
    }
}