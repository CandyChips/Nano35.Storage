using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateArticle
{
    public class CreateArticleTransactionErrorResult :
        ICreateArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateArticleTransaction :
        IPipelineNode<
            ICreateArticleRequestContract, 
            ICreateArticleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateArticleRequestContract,
            ICreateArticleResultContract> _nextNode;

        public CreateArticleTransaction(
            ApplicationContext context,
            IPipelineNode<
                ICreateArticleRequestContract, 
                ICreateArticleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input,
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
                return new CreateArticleTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}