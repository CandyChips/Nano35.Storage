using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdTransactionErrorResult :
        IUpdateCategoryParentCategoryIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateCategoryParentCategoryIdRequest :
        IPipelineNode<
            IUpdateCategoryParentCategoryIdRequestContract, 
            IUpdateCategoryParentCategoryIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateCategoryParentCategoryIdRequestContract,
            IUpdateCategoryParentCategoryIdResultContract> _nextNode;

        public TransactedUpdateCategoryParentCategoryIdRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateCategoryParentCategoryIdRequestContract, 
                IUpdateCategoryParentCategoryIdResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateCategoryParentCategoryIdResultContract> Ask(
            IUpdateCategoryParentCategoryIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await _nextNode.Ask(input, cancellationToken);
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