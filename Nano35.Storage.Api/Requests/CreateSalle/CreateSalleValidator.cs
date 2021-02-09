using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSalle
{
    public class CreateSalleValidatorErrorResult : ICreateSalleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateSalleValidator:
        IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract>
    {
        private readonly IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract> _nextNode;

        public CreateSalleValidator(
            IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateSalleResultContract> Ask(
            ICreateSalleRequestContract input)
        {
            if (false)
            {
                return new CreateSalleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}