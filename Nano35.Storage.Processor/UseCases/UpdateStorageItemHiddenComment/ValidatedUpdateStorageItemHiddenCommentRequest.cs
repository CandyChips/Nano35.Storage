using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentValidatorErrorResult : 
        IUpdateStorageItemHiddenCommentErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemHiddenCommentRequest:
        PipeNodeBase<
            IUpdateStorageItemHiddenCommentRequestContract,
            IUpdateStorageItemHiddenCommentResultContract>
    {
        public ValidatedUpdateStorageItemHiddenCommentRequest(
            IPipeNode<IUpdateStorageItemHiddenCommentRequestContract,
                IUpdateStorageItemHiddenCommentResultContract> next) : base(next)
        { }

        public override async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemHiddenCommentValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}