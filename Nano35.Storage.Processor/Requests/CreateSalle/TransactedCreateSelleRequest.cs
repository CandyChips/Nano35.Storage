using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateSalle
{
    public class CreateSelleTransactionErrorResult :
        ICreateSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateSelleRequest :
        IPipelineNode<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateSelleRequestContract,
            ICreateSelleResultContract> _nextNode;

        public TransactedCreateSelleRequest(
            ApplicationContext context,
            IPipelineNode<
                ICreateSelleRequestContract, 
                ICreateSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input,
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
                return new CreateSelleTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}