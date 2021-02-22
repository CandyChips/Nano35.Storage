using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetMoveDetails
{
    public class GetMoveDetailsByIdValidatorErrorResult : 
            
        IGetMoveDetailsByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetMoveDetailsByIdRequest:
        IPipelineNode<
            IGetMoveDetailsByIdRequestContract, 
            IGetMoveDetailsByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetMoveDetailsByIdRequestContract, 
            IGetMoveDetailsByIdResultContract> _nextNode;

        public ValidatedGetMoveDetailsByIdRequest(
            IPipelineNode<
                IGetMoveDetailsByIdRequestContract,
                IGetMoveDetailsByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetMoveDetailsByIdResultContract> Ask(
            IGetMoveDetailsByIdRequestContract input)
        {
            if (false)
            {
                return new GetMoveDetailsByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}