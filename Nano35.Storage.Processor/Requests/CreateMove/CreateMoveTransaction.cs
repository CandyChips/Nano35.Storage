using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateMove
{
    public class CreateComingTransactionErrorResult :
        ICreateComingErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateComingTransaction :
        IPipelineNode<
            ICreateComingRequestContract, 
            ICreateComingResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateComingRequestContract,
            ICreateComingResultContract> _nextNode;

        public CreateComingTransaction(
            ApplicationContext context,
            IPipelineNode<
                ICreateComingRequestContract, 
                ICreateComingResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input,
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
                return new CreateComingTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}