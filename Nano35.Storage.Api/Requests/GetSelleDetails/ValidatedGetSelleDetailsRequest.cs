using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetSelleDetails
{
    public class GetSelleDetailsByIdValidatorErrorResult : 
            
        IGetSelleDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetSelleDetailsRequest:
        IPipelineNode<
            IGetSelleDetailsByIdRequestContract, 
            IGetSelleDetailsByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetSelleDetailsByIdRequestContract, 
            IGetSelleDetailsByIdResultContract> _nextNode;

        public ValidatedGetSelleDetailsRequest(
            IPipelineNode<
                IGetSelleDetailsByIdRequestContract,
                IGetSelleDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetSelleDetailsByIdResultContract> Ask(
            IGetSelleDetailsByIdRequestContract input)
        {
            if (false)
            {
                return new GetSelleDetailsByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}