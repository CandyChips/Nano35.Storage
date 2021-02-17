using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class CreateCancellationValidatorErrorResult :
        ICreateCancellationErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCancellationRequest:
        IPipelineNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly IPipelineNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> _nextNode;

        public ValidatedCreateCancellationRequest(
            IPipelineNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input)
        {
            if (false)
            {
                return new CreateCancellationValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}