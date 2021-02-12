using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class GetAllArticlesValidatorErrorResult : 
        IGetAllArticlesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllArticlesValidator:
        IPipelineNode<
            IGetAllArticlesRequestContract,
            IGetAllArticlesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllArticlesRequestContract,
            IGetAllArticlesResultContract> _nextNode;

        public GetAllArticlesValidator(
            IPipelineNode<
                IGetAllArticlesRequestContract, 
                IGetAllArticlesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllArticlesResultContract> Ask(
            IGetAllArticlesRequestContract input)
        {
            if (false)
            {
                return new GetAllArticlesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}