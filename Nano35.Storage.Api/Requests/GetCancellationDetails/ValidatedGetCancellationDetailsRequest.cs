using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetCancellationDetails
{
    public class GetCancellationDetailsByIdValidatorErrorResult : 
            
        IGetCancellationDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetCancellationDetailsRequest:
        IPipelineNode<
            IGetCancellationDetailsByIdRequestContract, 
            IGetCancellationDetailsByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetCancellationDetailsByIdRequestContract, 
            IGetCancellationDetailsByIdResultContract> _nextNode;

        public ValidatedGetCancellationDetailsRequest(
            IPipelineNode<
                IGetCancellationDetailsByIdRequestContract,
                IGetCancellationDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetCancellationDetailsByIdResultContract> Ask(
            IGetCancellationDetailsByIdRequestContract input)
        {
            if (false)
            {
                return new GetCancellationDetailsByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}