using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllMoves
{
    public class GetAllMovesValidatorErrorResult :
        IGetAllMovesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllMovesRequest:
        IPipelineNode<
            IGetAllMovesRequestContract, 
            IGetAllMovesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllMovesRequestContract, 
            IGetAllMovesResultContract> _nextNode;

        public ValidatedGetAllMovesRequest(
            IPipelineNode<
                IGetAllMovesRequestContract,
                IGetAllMovesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllMovesResultContract> Ask(
            IGetAllMovesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllMovesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}