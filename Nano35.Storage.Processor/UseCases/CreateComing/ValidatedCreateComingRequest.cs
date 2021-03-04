using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateComing
{
    public class CreateComingValidatorErrorResult : ICreateComingErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateComingRequest:
        IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract> _nextNode;

        public ValidatedCreateComingRequest(
            IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateComingValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}