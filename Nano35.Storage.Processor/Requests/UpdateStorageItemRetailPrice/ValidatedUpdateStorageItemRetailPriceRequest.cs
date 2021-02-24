using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceValidatorErrorResult : 
        IUpdateStorageItemRetailPriceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemRetailPriceRequest:
        IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract,
            IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract, 
            IUpdateStorageItemRetailPriceResultContract> _nextNode;

        public ValidatedUpdateStorageItemRetailPriceRequest(
            IPipelineNode<
                IUpdateStorageItemRetailPriceRequestContract,
                IUpdateStorageItemRetailPriceResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemRetailPriceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}