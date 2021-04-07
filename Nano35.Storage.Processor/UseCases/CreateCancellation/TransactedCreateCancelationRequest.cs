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
        PipeNodeBase<
            ICreateCancellationRequestContract, 
            ICreateCancellationResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateCancellationRequest(
            ApplicationContext context,
            IPipeNode<ICreateCancellationRequestContract,
                ICreateCancellationResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var response = await DoNext(input, cancellationToken);
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