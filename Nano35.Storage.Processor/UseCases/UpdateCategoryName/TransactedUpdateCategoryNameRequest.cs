using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class UpdateCategoryNameTransactionErrorResult :
        IUpdateCategoryNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateCategoryNameRequest :
        PipeNodeBase<
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateCategoryNameRequest(
            ApplicationContext context,
            IPipeNode<IUpdateCategoryNameRequestContract,
                IUpdateCategoryNameResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input,
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
                return new UpdateCategoryNameTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}