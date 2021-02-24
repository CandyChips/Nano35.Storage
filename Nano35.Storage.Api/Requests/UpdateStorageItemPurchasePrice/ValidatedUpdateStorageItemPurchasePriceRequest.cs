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
        IPipelineNode<
            IUpdateStorageItemPurchasePriceRequestContract,
            IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly IPipelineNode<
            IUpdateStorageItemPurchasePriceRequestContract, 
            IUpdateStorageItemPurchasePriceResultContract> _nextNode;

        public ValidatedUpdateStorageItemPurchasePriceRequest(
            IPipelineNode<
                IUpdateStorageItemPurchasePriceRequestContract, 
                IUpdateStorageItemPurchasePriceResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input)
        {
            if (false)
            {
                return new UpdateStorageItemPurchasePriceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}