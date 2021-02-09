using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancelation
{
    public class CreateCancelationValidatorErrorResult :
        ICreateCancelationErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateCancelationValidator:
        IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract>
    {
        private readonly IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract> _nextNode;

        public CreateCancelationValidator(
            IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateCancelationResultContract> Ask(
            ICreateCancelationRequestContract input)
        {
            if (false)
            {
                return new CreateCancelationValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}