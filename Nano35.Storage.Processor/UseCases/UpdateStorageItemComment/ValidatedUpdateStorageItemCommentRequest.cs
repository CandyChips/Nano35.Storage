using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemComment
{
    public class ValidatedUpdateStorageItemCommentRequest:
        PipeNodeBase<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>
    {
        public ValidatedUpdateStorageItemCommentRequest(
            IPipeNode<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract> next) :
            base(next) { }

        public override async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}