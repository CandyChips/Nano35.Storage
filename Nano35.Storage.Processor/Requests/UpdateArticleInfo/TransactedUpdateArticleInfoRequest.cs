using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.UpdateArticleInfo
{
    public class UpdateArticleInfoTransactionErrorResult :
        IUpdateArticleInfoErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateArticleInfoRequest :
        IPipelineNode<
            IUpdateArticleInfoRequestContract, 
            IUpdateArticleInfoResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateArticleInfoRequestContract,
            IUpdateArticleInfoResultContract> _nextNode;

        public TransactedUpdateArticleInfoRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateArticleInfoRequestContract, 
                IUpdateArticleInfoResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input,
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
                return new UpdateArticleInfoTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}