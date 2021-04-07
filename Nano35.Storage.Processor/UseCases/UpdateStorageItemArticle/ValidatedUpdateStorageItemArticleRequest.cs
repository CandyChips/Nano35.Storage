using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleValidatorErrorResult : 
        IUpdateStorageItemArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemArticleRequest:
        PipeNodeBase<
            IUpdateStorageItemArticleRequestContract,
            IUpdateStorageItemArticleResultContract>
    {
        public ValidatedUpdateStorageItemArticleRequest(
            IPipeNode<IUpdateStorageItemArticleRequestContract,
                IUpdateStorageItemArticleResultContract> next) : base(next)
        {
        }

        public override async Task<IUpdateStorageItemArticleResultContract> Ask(
            IUpdateStorageItemArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemArticleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}