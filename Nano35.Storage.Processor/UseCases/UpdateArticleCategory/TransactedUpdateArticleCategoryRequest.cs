using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleCategory
{
    public class UpdateArticleCategoryTransactionErrorResult :
        IUpdateArticleCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateArticleCategoryRequest :
        PipeNodeBase<
            IUpdateArticleCategoryRequestContract, 
            IUpdateArticleCategoryResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateArticleCategoryRequest(
            ApplicationContext context,
            IPipeNode<IUpdateArticleCategoryRequestContract,
                IUpdateArticleCategoryResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input,
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
                return new UpdateArticleCategoryTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}