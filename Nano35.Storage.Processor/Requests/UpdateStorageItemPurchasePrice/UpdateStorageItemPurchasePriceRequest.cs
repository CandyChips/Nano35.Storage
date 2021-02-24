using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceRequest :
        IPipelineNode<
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
        
        public async Task<IUpdateStorageItemPurchasePriceResultContract> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.StorageItems
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            result.PurchasePrice = input.PurchasePrice;
            return new UpdateStorageItemPurchasePriceSuccessResultContract();
        }
    }
}