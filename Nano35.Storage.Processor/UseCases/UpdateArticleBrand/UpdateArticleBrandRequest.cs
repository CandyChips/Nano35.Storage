using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class UpdateArticleBrandRequest :
        EndPointNodeBase<
            IUpdateArticleBrandRequestContract, 
            IUpdateArticleBrandResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateArticleBrandRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateArticleBrandSuccessResultContract : 
            IUpdateArticleBrandSuccessResultContract
        {
            
        }
        
        public override async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Articles
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            result.Brand = input.Brand;
            return new UpdateArticleBrandSuccessResultContract();
        }
    }
}