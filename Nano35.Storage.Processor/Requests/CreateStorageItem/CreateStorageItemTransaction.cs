using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateStorageItem
{
    public class CreateStorageItemTransactionErrorResult :
        ICreateStorageItemErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateStorageItemTransaction :
        IPipelineNode<
            ICreateStorageItemRequestContract, 
            ICreateStorageItemResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateStorageItemRequestContract,
            ICreateStorageItemResultContract> _nextNode;

        public CreateStorageItemTransaction(
            ApplicationContext context,
            IPipelineNode<
                ICreateStorageItemRequestContract, 
                ICreateStorageItemResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input,
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
                return new CreateStorageItemTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}