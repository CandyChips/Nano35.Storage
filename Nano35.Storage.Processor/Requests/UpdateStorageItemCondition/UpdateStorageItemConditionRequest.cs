using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionRequest :
        IPipelineNode<
            IUpdateStorageItemConditionRequestContract, 
            IUpdateStorageItemConditionResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemConditionRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateStorageItemConditionSuccessResultContract : 
            IUpdateStorageItemConditionSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            result.ConditionId = input.ConditionId;
            
            return new UpdateStorageItemConditionSuccessResultContract();
        }
    }
}