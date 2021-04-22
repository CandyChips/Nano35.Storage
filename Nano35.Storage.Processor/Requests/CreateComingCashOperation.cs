using MassTransit;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Storage.Processor.UseCases;

namespace Nano35.Storage.Processor.Requests
{
    public class CreateComingCashOperation : 
        MasstransitRequest
        <ICreateComingCashOperationRequestContract, 
            ICreateComingCashOperationResultContract,
            ICreateComingCashOperationSuccessResultContract, 
            ICreateComingCashOperationErrorResultContract>
    {
        public CreateComingCashOperation(IBus bus, ICreateComingCashOperationRequestContract request) : base(bus, request) {}
    }
}