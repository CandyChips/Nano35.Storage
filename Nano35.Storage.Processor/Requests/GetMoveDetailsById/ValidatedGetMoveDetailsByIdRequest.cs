using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetMoveDetailsById
{
    public class GetMoveDetailsByIdValidatorErrorResult :
        IGetMoveDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetMoveDetailsByIdRequest:
        IPipelineNode<
            IGetMoveDetailsByIdRequestContract, 
            IGetMoveDetailsByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetMoveDetailsByIdRequestContract, 
            IGetMoveDetailsByIdResultContract> _nextNode;

        public ValidatedGetMoveDetailsByIdRequest(
            IPipelineNode<
                IGetMoveDetailsByIdRequestContract,
                IGetMoveDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetMoveDetailsByIdResultContract> Ask(
            IGetMoveDetailsByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetMoveDetailsByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}