using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceTransactionErrorResult :
        IUpdateStorageItemRetailPriceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemRetailPriceRequest :
        PipeNodeBase<
            IUpdateStorageItemRetailPriceRequestContract, 
            IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateStorageItemRetailPriceRequest(
            ApplicationContext context,
            IPipeNode<IUpdateStorageItemRetailPriceRequestContract,
                IUpdateStorageItemRetailPriceResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input,
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
                return new UpdateStorageItemRetailPriceTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}