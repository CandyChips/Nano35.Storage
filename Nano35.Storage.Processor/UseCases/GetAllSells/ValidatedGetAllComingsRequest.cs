using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllSells
{
    public class GetAllSellsValidatorErrorResult :
        IGetAllSellsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllSellsRequest:
        IPipelineNode<
            IGetAllSellsRequestContract, 
            IGetAllSellsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllSellsRequestContract, 
            IGetAllSellsResultContract> _nextNode;

        public ValidatedGetAllSellsRequest(
            IPipelineNode<
                IGetAllSellsRequestContract,
                IGetAllSellsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllSellsResultContract> Ask(
            IGetAllSellsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllSellsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}