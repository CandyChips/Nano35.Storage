using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllWarehousesOfItem
{
    public class GetAllWarehousesOfItemValidatorErrorResult :
        IGetAllWarehousesOfItemErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWarehousesOfItemRequest:
        IPipelineNode<
            IGetAllWarehousesOfItemRequestContract, 
            IGetAllWarehousesOfItemResultContract>
    {
        private readonly IPipelineNode<
            IGetAllWarehousesOfItemRequestContract, 
            IGetAllWarehousesOfItemResultContract> _nextNode;

        public ValidatedGetAllWarehousesOfItemRequest(
            IPipelineNode<
                IGetAllWarehousesOfItemRequestContract,
                IGetAllWarehousesOfItemResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllWarehousesOfItemResultContract> Ask(
            IGetAllWarehousesOfItemRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllWarehousesOfItemValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}