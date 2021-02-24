using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllWarehousesOfUnit
{
    public class GetAllWarehousesOfUnitValidatorErrorResult :
        IGetAllWarehousesOfUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWarehousesOfUnitRequest:
        IPipelineNode<
            IGetAllWarehousesOfUnitRequestContract, 
            IGetAllWarehousesOfUnitResultContract>
    {
        private readonly IPipelineNode<
            IGetAllWarehousesOfUnitRequestContract, 
            IGetAllWarehousesOfUnitResultContract> _nextNode;

        public ValidatedGetAllWarehousesOfUnitRequest(
            IPipelineNode<
                IGetAllWarehousesOfUnitRequestContract,
                IGetAllWarehousesOfUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllWarehousesOfUnitResultContract> Ask(
            IGetAllWarehousesOfUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllWarehousesOfUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}