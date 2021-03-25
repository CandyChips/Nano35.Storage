using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceValidatorErrorResult : 
        IUpdateStorageItemPurchasePriceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemPurchasePriceRequest:
        PipeNodeBase<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>
    {
        public ValidatedUpdateStorageItemPurchasePriceRequest(
            IPipeNode<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract> next) :
            base(next) { }

        public override async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input)
        {
            return await DoNext(input);
        }
    }
}