using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.CreateCancellation
{
    public class CreateCancellationValidatorErrorResult : 
        ICreateCancellationErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateCancellationValidator:
        IPipelineNode<
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract>
    {
        private readonly IPipelineNode<
            ICreateCancellationRequestContract, 
            ICreateCancellationResultContract> _nextNode;

        public CreateCancellationValidator(
            IPipelineNode<
                ICreateCancellationRequestContract,
                ICreateCancellationResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateCancellationValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}