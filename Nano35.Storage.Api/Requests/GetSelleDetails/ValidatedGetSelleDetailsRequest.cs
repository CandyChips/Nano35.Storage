using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetSelleDetails
{
    public class GetSelleDetailsValidatorErrorResult : 
            
        IGetSelleDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetSelleDetailsRequest:
        IPipelineNode<
            IGetSelleDetailsRequestContract, 
            IGetSelleDetailsResultContract>
    {
        private readonly IPipelineNode<
            IGetSelleDetailsRequestContract, 
            IGetSelleDetailsResultContract> _nextNode;

        public ValidatedGetSelleDetailsRequest(
            IPipelineNode<
                IGetSelleDetailsRequestContract,
                IGetSelleDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetSelleDetailsResultContract> Ask(
            IGetSelleDetailsRequestContract input)
        {
            if (false)
            {
                return new GetSelleDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}