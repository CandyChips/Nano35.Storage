using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class CreateSelleValidatorErrorResult : 
        ICreateSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateSelleRequest:
        IPipelineNode<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract>
    {
        private readonly IPipelineNode<
            ICreateSelleRequestContract,
            ICreateSelleResultContract> _nextNode;

        public ValidatedCreateSelleRequest(
            IPipelineNode<
                ICreateSelleRequestContract,
                ICreateSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input)
        {
            if (false)
            {
                return new CreateSelleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}