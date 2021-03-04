using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemRetailPrice
{
    public class UpdateStorageItemRetailPriceRequest :
        IPipelineNode<
            IUpdateStorageItemRetailPriceRequestContract, 
            IUpdateStorageItemRetailPriceResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemRetailPriceRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateStorageItemRetailPriceSuccessResultContract : 
            IUpdateStorageItemRetailPriceSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateStorageItemRetailPriceResultContract> Ask(
            IUpdateStorageItemRetailPriceRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            result.RetailPrice = input.RetailPrice;
            
            return new UpdateStorageItemRetailPriceSuccessResultContract();
        }
    }
}