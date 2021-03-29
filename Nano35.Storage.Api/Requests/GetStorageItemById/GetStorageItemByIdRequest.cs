using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class GetStorageItemByIdRequest:
        MasstransitRequest
        <IGetStorageItemByIdRequestContract,
            IGetStorageItemByIdResultContract,
            IGetStorageItemByIdSuccessResultContract,
            IGetStorageItemByIdErrorResultContract>
    {
        public GetStorageItemByIdRequest(IBus bus) : base(bus) {}
    }
}