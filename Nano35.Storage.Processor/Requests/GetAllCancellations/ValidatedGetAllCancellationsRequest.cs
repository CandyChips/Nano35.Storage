using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.GetAllCancellations
{
    public class GetAllCancellationsValidatorErrorResult :
        IGetAllCancellationsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCancellationsRequest:
        IPipelineNode<
            IGetAllCancellationsRequestContract, 
            IGetAllCancellationsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllCancellationsRequestContract, 
            IGetAllCancellationsResultContract> _nextNode;

        public ValidatedGetAllCancellationsRequest(
            IPipelineNode<
                IGetAllCancellationsRequestContract,
                IGetAllCancellationsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllCancellationsResultContract> Ask(
            IGetAllCancellationsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllCancellationsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}