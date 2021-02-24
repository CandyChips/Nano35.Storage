using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleValidatorErrorResult : 
        IUpdateStorageItemArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemArticleRequest:
        IPipelineNode<
            IUpdateStorageItemArticleRequestContract,
            IUpdateStorageItemArticleResultContract>
    {
        private readonly IPipelineNode<
            IUpdateStorageItemArticleRequestContract, 
            IUpdateStorageItemArticleResultContract> _nextNode;

        public ValidatedUpdateStorageItemArticleRequest(
            IPipelineNode<
                IUpdateStorageItemArticleRequestContract,
                IUpdateStorageItemArticleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateStorageItemArticleResultContract> Ask(
            IUpdateStorageItemArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemArticleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}