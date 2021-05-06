using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionRequest : UseCaseEndPointNodeBase<IUpdateStorageItemConditionRequestContract, IUpdateStorageItemConditionResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateStorageItemConditionRequest(ApplicationContext context)
        {
            _context = context;
        }
        public override async Task<UseCaseResponse<IUpdateStorageItemConditionResultContract>> Ask(
            IUpdateStorageItemConditionRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            
            if (result != null)
                return Pass("Не найдено");
            
            result.ConditionId = input.ConditionId;

            return Pass(new UpdateStorageItemConditionResultContract());
        }
    }
}