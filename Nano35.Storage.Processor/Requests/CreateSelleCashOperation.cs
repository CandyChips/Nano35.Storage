using MassTransit;
using Nano35.Contracts.Cashbox.Artifacts;
using Nano35.Storage.Processor.UseCases;

namespace Nano35.Storage.Processor.Requests
{
    public class CreateSelleCashOperation : 
        MasstransitRequest
        <ICreateSelleCashOperationRequestContract, 
            ICreateSelleCashOperationResultContract,
            ICreateSelleCashOperationSuccessResultContract, 
            ICreateSelleCashOperationErrorResultContract>
    {
        public CreateSelleCashOperation(IBus bus, ICreateSelleCashOperationRequestContract request) : base(bus, request) {}
    }
}