using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CheckExistArticle
{
    public class CheckExistArticleRequest :
        UseCaseEndPointNodeBase<ICheckExistArticleRequestContract, ICheckExistArticleResultContract>
    {
        private readonly ApplicationContext _context;

        public CheckExistArticleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<ICheckExistArticleResultContract>> Ask(
            ICheckExistArticleRequestContract input, 
            CancellationToken cancellationToken)
        {
            var result = await _context.Articles
                .FirstAsync(c => c.Id == input.ArticleId, cancellationToken: cancellationToken);

            if (result == null)
                return new UseCaseResponse<ICheckExistArticleResultContract>(new CheckExistArticleResultContract()
                    {Exist = false});
            return new UseCaseResponse<ICheckExistArticleResultContract>(new CheckExistArticleResultContract()
                {Exist = true});
        }
    }   
}