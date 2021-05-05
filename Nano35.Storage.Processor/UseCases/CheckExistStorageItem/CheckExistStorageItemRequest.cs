using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CheckExistStorageItem
{
    public class CheckExistStorageItemRequest :
        UseCaseEndPointNodeBase<ICheckExistStorageItemRequestContract, ICheckExistStorageItemResultContract>
    {
        private readonly ApplicationContext _context;

        public CheckExistStorageItemRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<ICheckExistStorageItemResultContract>> Ask(
            ICheckExistStorageItemRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems
                .FirstAsync(c => c.Id == input.StorageItemId, cancellationToken: cancellationToken);
            
            if (result == null)
                return new UseCaseResponse<ICheckExistStorageItemResultContract>(new CheckExistStorageItemResultContract()
                    {Exist = false});
            return new UseCaseResponse<ICheckExistStorageItemResultContract>(new CheckExistStorageItemResultContract()
                {Exist = true});
        }
    }   
}