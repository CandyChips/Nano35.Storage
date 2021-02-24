using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceTransactionErrorResult :
        IUpdateStorageItemRetailPriceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemRetailPriceRequest :
        IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract, 
            IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract,
            IUpdateStorageItemRetailPriceResultContract> _nextNode;

        public TransactedUpdateStorageItemRetailPriceRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateStorageItemRetailPriceRequestContract, 
                IUpdateStorageItemRetailPriceResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input,
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
                return new UpdateStorageItemRetailPriceTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}