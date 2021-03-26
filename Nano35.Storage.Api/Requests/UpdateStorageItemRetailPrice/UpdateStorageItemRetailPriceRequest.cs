using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceRequest :
        MasstransitRequest
        <IUpdateStorageItemRetailPriceRequestContract,
            IUpdateStorageItemRetailPriceResultContract,
            IUpdateStorageItemRetailPriceSuccessResultContract,
            IUpdateStorageItemRetailPriceErrorResultContract>
    {
        public UpdateStorageItemRetailPriceRequest(IBus bus) : base(bus) {}
    }
}