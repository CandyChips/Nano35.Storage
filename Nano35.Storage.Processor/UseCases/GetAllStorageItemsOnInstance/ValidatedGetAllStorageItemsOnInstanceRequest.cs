using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnInstance
{
    public class GetAllStorageItemsOnInstanceValidatorErrorResult :
        IGetAllStorageItemsOnInstanceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllStorageItemsOnInstanceRequest:
        IPipelineNode<
            IGetAllStorageItemsOnInstanceContract, 
            IGetAllStorageItemsOnInstanceResultContract>
    {
        private readonly IPipelineNode<
            IGetAllStorageItemsOnInstanceContract, 
            IGetAllStorageItemsOnInstanceResultContract> _nextNode;

        public ValidatedGetAllStorageItemsOnInstanceRequest(
            IPipelineNode<
                IGetAllStorageItemsOnInstanceContract,
                IGetAllStorageItemsOnInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllStorageItemsOnInstanceResultContract> Ask(
            IGetAllStorageItemsOnInstanceContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllStorageItemsOnInstanceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}