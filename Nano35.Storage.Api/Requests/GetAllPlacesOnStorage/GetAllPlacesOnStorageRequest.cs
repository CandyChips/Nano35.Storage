using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllPlacesOnStorage
{
    public class GetAllPlacesOnStorageRequest :
        MasstransitRequest<
            IGetAllPlacesOnStorageContract,
            IGetAllPlacesOnStorageResultContract,
            IGetAllPlacesOnStorageSuccessResultContract,
            IGetAllPlacesOnStorageErrorResultContract>
    {
        public GetAllPlacesOnStorageRequest(IBus bus) : base(bus) {}
    }
}