using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class CreateSelleTransactionErrorResult :
        ICreateSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateSelleRequest :
        PipeNodeBase<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateSelleRequest(
            ApplicationContext context,
            IPipeNode<ICreateSelleRequestContract,
                ICreateSelleResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input,
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
                return new CreateSelleTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}