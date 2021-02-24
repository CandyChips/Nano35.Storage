using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentValidatorErrorResult : 
        IUpdateStorageItemHiddenCommentErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemHiddenCommentRequest:
        IPipelineNode<
            IUpdateStorageItemHiddenCommentRequestContract,
            IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly IPipelineNode<
            IUpdateStorageItemHiddenCommentRequestContract, 
            IUpdateStorageItemHiddenCommentResultContract> _nextNode;

        public ValidatedUpdateStorageItemHiddenCommentRequest(
            IPipelineNode<
                IUpdateStorageItemHiddenCommentRequestContract,
                IUpdateStorageItemHiddenCommentResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemHiddenCommentValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}