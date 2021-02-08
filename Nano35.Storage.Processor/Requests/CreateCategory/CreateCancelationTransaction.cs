using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateCategory
{
    public class CreateCategoryTransactionErrorResult :
        ICreateCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateCategoryTransaction :
        IPipelineNode<
            ICreateCategoryRequestContract, 
            ICreateCategoryResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateCategoryRequestContract,
            ICreateCategoryResultContract> _nextNode;

        public CreateCategoryTransaction(
            ApplicationContext context,
            IPipelineNode<
                ICreateCategoryRequestContract, 
                ICreateCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input,
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
                return new CreateCategoryTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}