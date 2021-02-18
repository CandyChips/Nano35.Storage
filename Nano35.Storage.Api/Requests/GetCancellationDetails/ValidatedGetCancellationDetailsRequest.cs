using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetCancellationDetails
{
    public class GetCancellationDetailsValidatorErrorResult : 
            
        IGetCancellationDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetCancellationDetailsRequest:
        IPipelineNode<
            IGetCancellationDetailsRequestContract, 
            IGetCancellationDetailsResultContract>
    {
        private readonly IPipelineNode<
            IGetCancellationDetailsRequestContract, 
            IGetCancellationDetailsResultContract> _nextNode;

        public ValidatedGetCancellationDetailsRequest(
            IPipelineNode<
                IGetCancellationDetailsRequestContract,
                IGetCancellationDetailsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetCancellationDetailsResultContract> Ask(
            IGetCancellationDetailsRequestContract input)
        {
            if (false)
            {
                return new GetCancellationDetailsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}