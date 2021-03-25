using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllMoveDetails
{
    public class GetAllMoveDetailsValidatorErrorResult :
        IGetAllMoveDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllMoveDetailsRequest:
        IPipelineNode<
            IGetAllMoveDetailsRequestContract, 
            IGetAllMoveDetailsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllMoveDetailsRequestContract, 
            IGetAllMoveDetailsResultContract> _nextNode;

        public ValidatedGetAllMoveDetailsRequest(
            IPipelineNode<
                IGetAllMoveDetailsRequestContract,
                IGetAllMoveDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllMoveDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}