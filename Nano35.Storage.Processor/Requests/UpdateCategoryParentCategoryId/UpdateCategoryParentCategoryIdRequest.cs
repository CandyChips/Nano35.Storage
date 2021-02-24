using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdRequest :
        IPipelineNode<
            IUpdateCategoryParentCategoryIdRequestContract, 
            IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateCategoryParentCategoryIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateCategoryParentCategoryIdSuccessResultContract : 
            IUpdateCategoryParentCategoryIdSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Categories
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            result.ParentCategoryId = input.ParentCategoryId;
            return new UpdateCategoryParentCategoryIdSuccessResultContract();
        }
    }
}