using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdTransactionErrorResult :
        IUpdateCategoryParentCategoryIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateCategoryParentCategoryIdRequest :
        PipeNodeBase<
            IUpdateCategoryParentCategoryIdRequestContract, 
            IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateCategoryParentCategoryIdRequest(
            ApplicationContext context,
            IPipeNode<IUpdateCategoryParentCategoryIdRequestContract,
                IUpdateCategoryParentCategoryIdResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return response;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return new UpdateCategoryParentCategoryIdTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}