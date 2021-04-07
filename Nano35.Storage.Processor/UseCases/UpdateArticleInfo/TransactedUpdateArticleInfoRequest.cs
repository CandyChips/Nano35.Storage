using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleInfo
{
    public class UpdateArticleInfoTransactionErrorResult :
        IUpdateArticleInfoErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateArticleInfoRequest :
        PipeNodeBase<
            IUpdateArticleInfoRequestContract, 
            IUpdateArticleInfoResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateArticleInfoRequest(
            ApplicationContext context,
            IPipeNode<IUpdateArticleInfoRequestContract,
                IUpdateArticleInfoResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input,
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
                return new UpdateArticleInfoTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}