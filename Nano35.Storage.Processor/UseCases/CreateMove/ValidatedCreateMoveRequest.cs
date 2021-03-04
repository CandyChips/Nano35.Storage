using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateMove
{
    public class CreateMoveValidatorErrorResult : 
        ICreateMoveErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateMoveRequest:
        IPipelineNode<
            ICreateMoveRequestContract, 
            ICreateMoveResultContract>
    {
        private readonly IPipelineNode<
            ICreateMoveRequestContract, 
            ICreateMoveResultContract> _nextNode;

        public ValidatedCreateMoveRequest(
            IPipelineNode<
                ICreateMoveRequestContract, 
                ICreateMoveResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateMoveValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}