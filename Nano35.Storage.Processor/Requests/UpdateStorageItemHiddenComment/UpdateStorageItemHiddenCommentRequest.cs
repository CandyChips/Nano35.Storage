using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemHiddenComment
{
    public class UpdateStorageItemHiddenCommentRequest :
        IPipelineNode<
            IUpdateStorageItemHiddenCommentRequestContract, 
            IUpdateStorageItemHiddenCommentResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemHiddenCommentRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateStorageItemHiddenCommentSuccessResultContract : 
            IUpdateStorageItemHiddenCommentSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateStorageItemHiddenCommentResultContract> Ask(
            IUpdateStorageItemHiddenCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            result.HiddenComment = input.HiddenComment;
            return new UpdateStorageItemHiddenCommentSuccessResultContract();
        }
    }
}