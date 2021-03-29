using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllWarehousesOfUnit
{
    public class GetAllWarehousesOfUnitRequest :
        MasstransitRequest
        <IGetAllWarehousesOfUnitRequestContract,
            IGetAllWarehousesOfUnitResultContract,
            IGetAllWarehousesOfUnitSuccessResultContract,
            IGetAllWarehousesOfUnitErrorResultContract>
    {
        public GetAllWarehousesOfUnitRequest(IBus bus) : base(bus) {}
    }
}