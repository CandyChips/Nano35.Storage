using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.UseCases;

namespace Nano35.Storage.Processor.Requests
{
    public class GetAllStorageItems : 
        MasstransitRequest
        <IGetAllStorageItemsRequestContract, 
            IGetAllStorageItemsResultContract,
            IGetAllStorageItemsSuccessResultContract, 
            IGetAllStorageItemsErrorResultContract>
    {
        public GetAllStorageItems(IBus bus, IGetAllStorageItemsRequestContract request) : base(bus, request) {}
    }
}