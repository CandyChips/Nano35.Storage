using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentTransactionErrorResult :
        IUpdateStorageItemCommentErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemCommentRequest :
        PipeNodeBase<
            IUpdateStorageItemCommentRequestContract, 
            IUpdateStorageItemCommentResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateStorageItemCommentRequest(
            ApplicationContext context,
            IPipeNode<IUpdateStorageItemCommentRequestContract,
                IUpdateStorageItemCommentResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input,
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
                return new UpdateStorageItemCommentTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}