using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Storage.Processor.UseCases;

namespace Nano35.Storage.Processor.Requests
{
    public class GetUnitStringById : 
        MasstransitRequest
        <IGetUnitStringByIdRequestContract, 
            IGetUnitStringByIdResultContract,
            IGetUnitStringByIdSuccessResultContract, 
            IGetUnitStringByIdErrorResultContract>
    {
        public GetUnitStringById(IBus bus, IGetUnitStringByIdRequestContract request) : base(bus, request) {}
    }
}