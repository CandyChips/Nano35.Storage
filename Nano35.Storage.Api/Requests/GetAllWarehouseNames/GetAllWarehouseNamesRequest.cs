using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehouseNames
{
    public class GetAllWarehouseNamesRequest :
        MasstransitRequest
        <IGetAllWarehouseNamesRequestContract,
            IGetAllWarehouseNamesResultContract,
            IGetAllWarehouseNamesSuccessResultContract,
            IGetAllWarehouseNamesErrorResultContract>
    {
        public GetAllWarehouseNamesRequest(IBus bus) : base(bus) {}
    }
}