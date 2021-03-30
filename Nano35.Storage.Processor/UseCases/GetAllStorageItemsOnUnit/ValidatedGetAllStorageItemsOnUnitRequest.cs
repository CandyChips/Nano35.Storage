using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllStorageItemsOnUnit
{
    public class GetAllStorageItemsOnUnitValidatorErrorResult :
        IGetAllStorageItemsOnUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllStorageItemsOnUnitRequest:
        IPipelineNode<
            IGetAllStorageItemsOnUnitContract, 
            IGetAllStorageItemsOnUnitResultContract>
    {
        private readonly IPipelineNode<
            IGetAllStorageItemsOnUnitContract, 
            IGetAllStorageItemsOnUnitResultContract> _nextNode;

        public ValidatedGetAllStorageItemsOnUnitRequest(
            IPipelineNode<
                IGetAllStorageItemsOnUnitContract,
                IGetAllStorageItemsOnUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllStorageItemsOnUnitResultContract> Ask(
            IGetAllStorageItemsOnUnitContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllStorageItemsOnUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}