using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateArticle
{
    public class CreateArticleValidatorErrorResult :
        ICreateArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateArticleRequest:
        IPipelineNode<
            ICreateArticleRequestContract, 
            ICreateArticleResultContract>
    {
        private readonly IPipelineNode<
            ICreateArticleRequestContract, 
            ICreateArticleResultContract> _nextNode;

        public ValidatedCreateArticleRequest(
            IPipelineNode<
                ICreateArticleRequestContract, 
                ICreateArticleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateArticleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}