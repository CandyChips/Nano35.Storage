using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateStorageItem
{
    public class CreateStorageItemRequest :
        IPipelineNode<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateStorageItemRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class CreateStorageItemSuccessResultContract : 
            ICreateStorageItemSuccessResultContract
        {
            
        }
        
        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input,
            CancellationToken cancellationToken)
        {
            var item = new StorageItem()
            {
                Id = input.NewId,
                Comment = input.Comment,
                ArticleId = input.ArticleId,
                ConditionId = input.ConditionId,
                HiddenComment = input.HiddenComment,
                InstanceId = input.InstanceId,
                IsDeleted = false,
                PurchasePrice = input.PurchasePrice,
                RetailPrice = input.RetailPrice
            };
            
            await _context.StorageItems.AddAsync(item, cancellationToken);
                    
            return new CreateStorageItemSuccessResultContract();
        }
    }
}