using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseOfStorageItem
{
    public class GetAllWarehouseOfStorageItemValidatorErrorResult :
        IGetAllWarehouseOfStorageItemErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWarehouseOfStorageItemRequest:
        IPipelineNode<
            IGetAllWarehouseOfStorageItemRequestContract, 
            IGetAllWarehouseOfStorageItemResultContract>
    {
        private readonly IPipelineNode<
            IGetAllWarehouseOfStorageItemRequestContract, 
            IGetAllWarehouseOfStorageItemResultContract> _nextNode;

        public ValidatedGetAllWarehouseOfStorageItemRequest(
            IPipelineNode<
                IGetAllWarehouseOfStorageItemRequestContract,
                IGetAllWarehouseOfStorageItemResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllWarehouseOfStorageItemResultContract> Ask(
            IGetAllWarehouseOfStorageItemRequestContract input,
            CancellationToken cancellationToken)
        {
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}