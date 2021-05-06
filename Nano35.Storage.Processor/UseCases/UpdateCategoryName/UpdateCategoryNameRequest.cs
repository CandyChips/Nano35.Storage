using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class UpdateCategoryNameRequest :
        UseCaseEndPointNodeBase<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateCategoryNameRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IUpdateCategoryNameResultContract>> Ask(
            IUpdateCategoryNameRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            
            if (result != null)
                return Pass("Не найдено");
            
            result.Name = input.Name;

            return Pass(new UpdateCategoryNameResultContract());
        }
    }
}