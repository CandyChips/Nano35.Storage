using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemComment
{
    public class UpdateStorageItemCommentRequest :
        UseCaseEndPointNodeBase<IUpdateStorageItemCommentRequestContract, IUpdateStorageItemCommentResultContract>
    {
        private readonly ApplicationContext _context;

        public UpdateStorageItemCommentRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<IUpdateStorageItemCommentResultContract>> Ask(
            IUpdateStorageItemCommentRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.StorageItems.FirstOrDefaultAsync(a => a.Id == input.Id, cancellationToken);
            
            if (result != null)
                return Pass("Не найдено");
            
            result.Comment = input.Comment;

            return Pass(new UpdateStorageItemCommentResultContract());
        }
    }
}