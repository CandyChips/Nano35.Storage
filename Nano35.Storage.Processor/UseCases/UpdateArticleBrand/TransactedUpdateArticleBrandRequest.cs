using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class UpdateArticleBrandTransactionErrorResult :
        IUpdateArticleBrandErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateArticleBrandRequest :
        IPipelineNode<
            IUpdateArticleBrandRequestContract, 
            IUpdateArticleBrandResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateArticleBrandRequestContract,
            IUpdateArticleBrandResultContract> _nextNode;

        public TransactedUpdateArticleBrandRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateArticleBrandRequestContract, 
                IUpdateArticleBrandResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input,
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
                return new UpdateArticleBrandTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}