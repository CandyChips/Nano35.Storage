using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseOfStorageItem
{
    public class GetAllWarehouseOfStorageItemRequest :
        MasstransitRequest
        <IGetAllWarehouseOfStorageItemRequestContract,
            IGetAllWarehouseOfStorageItemResultContract,
            IGetAllWarehouseOfStorageItemSuccessResultContract,
            IGetAllWarehouseOfStorageItemErrorResultContract>
    {
        public GetAllWarehouseOfStorageItemRequest(IBus bus) : base(bus) {}
    }
}