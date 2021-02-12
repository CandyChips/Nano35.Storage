using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class GetAllComingsValidatorErrorResult : 
        IGetAllComingsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllComingsValidator:
        IPipelineNode<
            IGetAllComingsRequestContract, 
            IGetAllComingsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllComingsRequestContract, 
            IGetAllComingsResultContract> _nextNode;

        public GetAllComingsValidator(
            IPipelineNode<
                IGetAllComingsRequestContract,
                IGetAllComingsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllComingsResultContract> Ask(
            IGetAllComingsRequestContract input)
        {
            if (false)
            {
                return new GetAllComingsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}