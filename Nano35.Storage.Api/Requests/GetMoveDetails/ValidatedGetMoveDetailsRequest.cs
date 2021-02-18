using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetMoveDetails
{
    public class GetMoveDetailsValidatorErrorResult : 
            
        IGetMoveDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetMoveDetailsRequest:
        IPipelineNode<
            IGetMoveDetailsRequestContract, 
            IGetMoveDetailsResultContract>
    {
        private readonly IPipelineNode<
            IGetMoveDetailsRequestContract, 
            IGetMoveDetailsResultContract> _nextNode;

        public ValidatedGetMoveDetailsRequest(
            IPipelineNode<
                IGetMoveDetailsRequestContract,
                IGetMoveDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetMoveDetailsResultContract> Ask(
            IGetMoveDetailsRequestContract input)
        {
            if (false)
            {
                return new GetMoveDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}