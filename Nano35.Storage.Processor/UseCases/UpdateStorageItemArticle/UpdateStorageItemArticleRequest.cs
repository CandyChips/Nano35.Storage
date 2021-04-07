using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleRequest :
        EndPointNodeBase<
            IUpdateStorageItemArticleRequestContract, 
            IUpdateStorageItemArticleResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemArticleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class UpdateStorageItemArticleSuccessResultContract : 
            IUpdateStorageItemArticleSuccessResultContract
        {
            
        }
        
        public override async Task<IUpdateStorageItemArticleResultContract> Ask(
            IUpdateStorageItemArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            result.ArticleId = input.ArticleId;
            
            return new UpdateStorageItemArticleSuccessResultContract();
        }
    }
}