using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class UpdateCategoryNameTransactionErrorResult :
        IUpdateCategoryNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateCategoryNameRequest :
        IPipelineNode<
            IUpdateCategoryNameRequestContract, 
            IUpdateCategoryNameResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateCategoryNameRequestContract,
            IUpdateCategoryNameResultContract> _nextNode;

        public TransactedUpdateCategoryNameRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateCategoryNameRequestContract, 
                IUpdateCategoryNameResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input,
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
                return new UpdateCategoryNameTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}