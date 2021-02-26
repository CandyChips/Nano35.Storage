using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentRequest :
        IPipelineNode<
            IUpdateStorageItemCommentRequestContract, 
            IUpdateStorageItemCommentResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemCommentRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateStorageItemCommentSuccessResultContract : 
            IUpdateStorageItemCommentSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateStorageItemCommentResultContract> Ask(
            IUpdateStorageItemCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            result.Comment = input.Comment;
            
            return new UpdateStorageItemCommentSuccessResultContract();
        }
    }
}