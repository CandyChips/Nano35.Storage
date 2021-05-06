using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemPurchasePrice
{
    public class UpdateStorageItemPurchasePriceRequest :
        UseCaseEndPointNodeBase<IUpdateStorageItemPurchasePriceRequestContract, IUpdateStorageItemPurchasePriceResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemPurchasePriceRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        public override async Task<UseCaseResponse<IUpdateStorageItemPurchasePriceResultContract>> Ask(
            IUpdateStorageItemPurchasePriceRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            
            if (result != null)
                return Pass("Не найдено");
            
            result.PurchasePrice = input.PurchasePrice;
            return Pass(new UpdateStorageItemPurchasePriceResultContract());
        }
    }
}