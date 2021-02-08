using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleValidatorErrorResult : ICreateArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateArticleValidator:
        IPipelineNode<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly IPipelineNode<ICreateArticleRequestContract, ICreateArticleResultContract> _nextNode;

        public CreateArticleValidator(
            IPipelineNode<ICreateArticleRequestContract, ICreateArticleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input)
        {
            if (false)
            {
                return new CreateArticleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}