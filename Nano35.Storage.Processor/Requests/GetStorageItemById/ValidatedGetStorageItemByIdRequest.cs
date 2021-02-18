using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetStorageItemById
{
    public class GetStorageItemByIdValidatorErrorResult :
        IGetStorageItemByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetStorageItemByIdRequest:
        IPipelineNode<
            IGetStorageItemByIdRequestContract, 
            IGetStorageItemByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetStorageItemByIdRequestContract, 
            IGetStorageItemByIdResultContract> _nextNode;

        public ValidatedGetStorageItemByIdRequest(
            IPipelineNode<
                IGetStorageItemByIdRequestContract,
                IGetStorageItemByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetStorageItemByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}