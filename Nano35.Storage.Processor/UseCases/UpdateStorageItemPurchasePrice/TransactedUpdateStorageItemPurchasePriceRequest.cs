using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceTransactionErrorResult :
        IUpdateStorageItemPurchasePriceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemPurchasePriceRequest :
        PipeNodeBase<
            IUpdateStorageItemPurchasePriceRequestContract, 
            IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateStorageItemPurchasePriceRequest(
            ApplicationContext context,
            IPipeNode<IUpdateStorageItemPurchasePriceRequestContract,
                IUpdateStorageItemPurchasePriceResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input,
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
                return new UpdateStorageItemPurchasePriceTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}