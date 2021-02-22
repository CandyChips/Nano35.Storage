using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateArticleModel
{
    public class UpdateArticleModelTransactionErrorResult :
        IUpdateArticleModelErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateArticleModelRequest :
        IPipelineNode<
            IUpdateArticleModelRequestContract, 
            IUpdateArticleModelResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateArticleModelRequestContract,
            IUpdateArticleModelResultContract> _nextNode;

        public TransactedUpdateArticleModelRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateArticleModelRequestContract, 
                IUpdateArticleModelResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input,
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
                return new UpdateArticleModelTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}