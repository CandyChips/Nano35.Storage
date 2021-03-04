using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentValidatorErrorResult : 
        IUpdateStorageItemCommentErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateStorageItemCommentRequest:
        IPipelineNode<
            IUpdateStorageItemCommentRequestContract,
            IUpdateStorageItemCommentResultContract>
    {
        private readonly IPipelineNode<
            IUpdateStorageItemCommentRequestContract, 
            IUpdateStorageItemCommentResultContract> _nextNode;

        public ValidatedUpdateStorageItemCommentRequest(
            IPipelineNode<
                IUpdateStorageItemCommentRequestContract,
                IUpdateStorageItemCommentResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateStorageItemCommentValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}