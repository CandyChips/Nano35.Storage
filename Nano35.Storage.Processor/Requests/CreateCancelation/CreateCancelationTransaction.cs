using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateCancelation
{
    public class CreateCancelationTransactionErrorResult :
        ICreateCancelationErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateCancelationTransaction :
        IPipelineNode<
            ICreateCancelationRequestContract, 
            ICreateCancelationResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateCancelationRequestContract,
            ICreateCancelationResultContract> _nextNode;

        public CreateCancelationTransaction(
            ApplicationContext context,
            IPipelineNode<
                ICreateCancelationRequestContract, 
                ICreateCancelationResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateCancelationResultContract> Ask(
            ICreateCancelationRequestContract input,
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
                return new CreateCancelationTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}