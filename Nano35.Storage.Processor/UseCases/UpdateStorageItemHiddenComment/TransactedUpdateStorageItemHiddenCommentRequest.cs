using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentTransactionErrorResult :
        IUpdateStorageItemHiddenCommentErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemHiddenCommentRequest :
        PipeNodeBase<
            IUpdateStorageItemHiddenCommentRequestContract, 
            IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateStorageItemHiddenCommentRequest(
            ApplicationContext context,
            IPipeNode<IUpdateStorageItemHiddenCommentRequestContract,
                IUpdateStorageItemHiddenCommentResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return new UpdateStorageItemHiddenCommentTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}