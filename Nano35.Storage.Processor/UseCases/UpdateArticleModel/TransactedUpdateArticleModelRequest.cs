using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleModel
{
    public class UpdateArticleModelTransactionErrorResult :
        IUpdateArticleModelErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateArticleModelRequest :
        PipeNodeBase<
            IUpdateArticleModelRequestContract, 
            IUpdateArticleModelResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateArticleModelRequest(
            ApplicationContext context,
            IPipeNode<IUpdateArticleModelRequestContract,
                IUpdateArticleModelResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input,
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
                return new UpdateArticleModelTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}