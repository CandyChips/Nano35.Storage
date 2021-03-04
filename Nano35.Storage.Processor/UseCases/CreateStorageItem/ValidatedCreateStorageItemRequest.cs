using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateStorageItem
{
    public class CreateStorageItemValidatorErrorResult : 
        ICreateStorageItemErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateStorageItemRequest:
        IPipelineNode<
            ICreateStorageItemRequestContract,
            ICreateStorageItemResultContract>
    {
        private readonly IPipelineNode<
            ICreateStorageItemRequestContract, 
            ICreateStorageItemResultContract> _nextNode;

        public ValidatedCreateStorageItemRequest(
            IPipelineNode<
                ICreateStorageItemRequestContract,
                ICreateStorageItemResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateStorageItemValidatorErrorResult() 
                    {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}