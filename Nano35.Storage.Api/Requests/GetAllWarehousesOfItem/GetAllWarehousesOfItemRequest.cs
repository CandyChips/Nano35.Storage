using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehousesOfItem
{
    public class GetAllWarehousesOfItemRequest :
        MasstransitRequest
        <IGetAllWarehousesOfItemRequestContract,
            IGetAllWarehousesOfItemResultContract,
            IGetAllWarehousesOfItemSuccessResultContract,
            IGetAllWarehousesOfItemErrorResultContract>
    {
        public GetAllWarehousesOfItemRequest(IBus bus) : base(bus) {}
    }
}