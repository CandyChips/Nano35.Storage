using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleCategory
{
    public class UpdateArticleCategoryRequest : UseCaseEndPointNodeBase<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateArticleCategoryRequest(ApplicationContext context)
        {
            _context = context;
        }
        public override async Task<UseCaseResponse<IUpdateArticleCategoryResultContract>> Ask(
            IUpdateArticleCategoryRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.Articles.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken));
            
            if (result == null)
                return Pass("Не найдено");
            
            result.CategoryId = input.CategoryId;
            return Pass(new UpdateArticleCategoryResultContract());
        }
    }
}