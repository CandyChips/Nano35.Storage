using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleCategory
{
    public class UpdateArticleCategoryTransactionErrorResult :
        IUpdateArticleCategoryErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateArticleCategoryRequest :
        IPipelineNode<
            IUpdateArticleCategoryRequestContract, 
            IUpdateArticleCategoryResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateArticleCategoryRequestContract,
            IUpdateArticleCategoryResultContract> _nextNode;

        public TransactedUpdateArticleCategoryRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateArticleCategoryRequestContract, 
                IUpdateArticleCategoryResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input,
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
                return new UpdateArticleCategoryTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}