using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class UpdateCategoryNameRequest :
        IPipelineNode<
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateCategoryNameRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateCategoryNameSuccessResultContract : 
            IUpdateCategoryNameSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            result.Name = input.Name;
            
            return new UpdateCategoryNameSuccessResultContract();
        }
    }
}