using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleModel
{
    public class UpdateArticleModelRequest :
        EndPointNodeBase<
            IUpdateArticleModelRequestContract, 
            IUpdateArticleModelResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateArticleModelRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateArticleModelSuccessResultContract : 
            IUpdateArticleModelSuccessResultContract
        {
            
        }
        
        public override async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Articles
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            result.Model = input.Model;
            return new UpdateArticleModelSuccessResultContract();
        }
    }
}