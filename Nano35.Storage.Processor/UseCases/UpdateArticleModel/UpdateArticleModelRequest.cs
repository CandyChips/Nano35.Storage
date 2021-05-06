using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleModel
{
    public class UpdateArticleModelRequest :
        UseCaseEndPointNodeBase<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateArticleModelRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IUpdateArticleModelResultContract>> Ask(
            IUpdateArticleModelRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Articles
                .FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            
            if (result != null)
                return Pass("Не найдено");
            
            result.Model = input.Model;
            return Pass(new UpdateArticleModelResultContract());
        }
    }
}