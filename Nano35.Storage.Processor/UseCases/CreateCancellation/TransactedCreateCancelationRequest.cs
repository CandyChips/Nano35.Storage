using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class CreateCancellationTransactionErrorResult :
        ICreateCancellationErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateCancellationRequest :
        IPipelineNode<
            ICreateCancellationRequestContract, 
            ICreateCancellationResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract> _nextNode;

        public TransactedCreateCancellationRequest(
            ApplicationContext context,
            IPipelineNode<
                ICreateCancellationRequestContract, 
                ICreateCancellationResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
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
                return new CreateCancellationTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}