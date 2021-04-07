using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceRequest :
        EndPointNodeBase<
            IUpdateStorageItemPurchasePriceRequestContract, 
            IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemPurchasePriceRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateStorageItemPurchasePriceSuccessResultContract : 
            IUpdateStorageItemPurchasePriceSuccessResultContract
        {
            
        }
        
        public override async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            result.PurchasePrice = input.PurchasePrice;
            return new UpdateStorageItemPurchasePriceSuccessResultContract();
        }
    }
}