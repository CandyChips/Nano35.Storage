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
        PipeNodeBase<
            IUpdateArticleCategoryRequestContract,
            IUpdateArticleCategoryResultContract>
    {
        public ValidatedUpdateArticleCategoryRequest(
            IPipeNode<IUpdateArticleCategoryRequestContract,
                IUpdateArticleCategoryResultContract> next) : base(next)
        {
        }

        public override async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateArticleCategoryValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}