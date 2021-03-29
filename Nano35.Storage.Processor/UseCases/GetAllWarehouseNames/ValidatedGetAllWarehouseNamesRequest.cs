using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseNames
{
    public class GetAllWarehouseNamesValidatorErrorResult :
        IGetAllWarehouseNamesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWarehouseNamesRequest:
        IPipelineNode<
            IGetAllWarehouseNamesRequestContract, 
            IGetAllWarehouseNamesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllWarehouseNamesRequestContract, 
            IGetAllWarehouseNamesResultContract> _nextNode;

        public ValidatedGetAllWarehouseNamesRequest(
            IPipelineNode<
                IGetAllWarehouseNamesRequestContract,
                IGetAllWarehouseNamesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllWarehouseNamesResultContract> Ask(
            IGetAllWarehouseNamesRequestContract input,
            CancellationToken cancellationToken)
        {
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}