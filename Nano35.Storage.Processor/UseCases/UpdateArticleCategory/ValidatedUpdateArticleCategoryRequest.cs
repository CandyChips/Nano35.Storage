using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleCategory
{
    public class UpdateArticleCategoryValidatorErrorResult : 
        IUpdateArticleCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateArticleCategoryRequest:
        IPipelineNode<
            IUpdateArticleCategoryRequestContract,
            IUpdateArticleCategoryResultContract>
    {
        private readonly IPipelineNode<
            IUpdateArticleCategoryRequestContract, 
            IUpdateArticleCategoryResultContract> _nextNode;

        public ValidatedUpdateArticleCategoryRequest(
            IPipelineNode<
                IUpdateArticleCategoryRequestContract,
                IUpdateArticleCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateArticleCategoryValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}