using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleInfo
{
    public class UpdateArticleInfoRequest :
        IPipelineNode<
            IUpdateArticleInfoRequestContract, 
            IUpdateArticleInfoResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateArticleInfoRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateArticleInfoSuccessResultContract : 
            IUpdateArticleInfoSuccessResultContract
        {
            
        }
        
        public async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Articles
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            result.Info = input.Info;
            return new UpdateArticleInfoSuccessResultContract();
        }
    }
}