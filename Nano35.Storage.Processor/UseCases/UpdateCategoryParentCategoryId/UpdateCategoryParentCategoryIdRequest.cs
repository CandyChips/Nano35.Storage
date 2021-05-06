using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdRequest :
        UseCaseEndPointNodeBase<IUpdateCategoryParentCategoryIdRequestContract, IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateCategoryParentCategoryIdRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IUpdateCategoryParentCategoryIdResultContract>> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            
            if (result != null)
                return Pass("Не найдено");
            
            result.ParentCategoryId = input.ParentCategoryId;

            return Pass(new UpdateCategoryParentCategoryIdResultContract());
        }
    }
}