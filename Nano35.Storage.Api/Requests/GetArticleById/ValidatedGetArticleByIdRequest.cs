using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdValidatorErrorResult : 
        IGetArticleByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetArticleByIdRequest:
        IPipelineNode<
            IGetArticleByIdRequestContract,
            IGetArticleByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetArticleByIdRequestContract, 
            IGetArticleByIdResultContract> _nextNode;

        public ValidatedGetArticleByIdRequest(
            IPipelineNode<
                IGetArticleByIdRequestContract, 
                IGetArticleByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input)
        {
            if (false)
            {
                return new GetArticleByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}