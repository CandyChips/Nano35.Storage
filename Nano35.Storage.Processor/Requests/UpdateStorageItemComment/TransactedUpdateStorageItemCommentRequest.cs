using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentTransactionErrorResult :
        IUpdateStorageItemCommentErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemCommentRequest :
        IPipelineNode<
            IUpdateStorageItemCommentRequestContract, 
            IUpdateStorageItemCommentResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateStorageItemCommentRequestContract,
            IUpdateStorageItemCommentResultContract> _nextNode;

        public TransactedUpdateStorageItemCommentRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateStorageItemCommentRequestContract, 
                IUpdateStorageItemCommentResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await _nextNode.Ask(input, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return new UpdateStorageItemCommentTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}