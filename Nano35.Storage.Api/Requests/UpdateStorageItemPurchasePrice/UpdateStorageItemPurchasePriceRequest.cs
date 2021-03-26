using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceRequest :
        MasstransitRequest
        <IUpdateStorageItemPurchasePriceRequestContract,
            IUpdateStorageItemPurchasePriceResultContract,
            IUpdateStorageItemPurchasePriceSuccessResultContract,
            IUpdateStorageItemPurchasePriceErrorResultContract>
    {
        public UpdateStorageItemPurchasePriceRequest(IBus bus) : base(bus) {}
    }
}