using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemRequest :
        MasstransitRequest<
            ICreateStorageItemRequestContract,
            ICreateStorageItemResultContract,
            ICreateStorageItemSuccessResultContract,
            ICreateStorageItemErrorResultContract>
    {
        public CreateStorageItemRequest(IBus bus) : base(bus) {}
    }
}