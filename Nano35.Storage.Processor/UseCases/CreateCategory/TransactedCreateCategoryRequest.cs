using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCategory
{
    public class CreateCategoryTransactionErrorResult :
        ICreateCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateCategoryRequest :
        PipeNodeBase<
            ICreateCategoryRequestContract, 
            ICreateCategoryResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateCategoryRequest(
            ApplicationContext context,
            IPipeNode<ICreateCategoryRequestContract,
                ICreateCategoryResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input,
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
                return new CreateCategoryTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}