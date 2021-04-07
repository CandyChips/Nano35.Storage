using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases
{
    public class Error : IError
    {
        public string Message { get; set; }
    }
    
    public class TransactedPipeNode<TIn, TOut> : PipeNodeBase<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly ApplicationContext _context;

        public TransactedPipeNode(
            ApplicationContext context,
            IPipeNode<TIn, TOut> next) : base(next)
        {
            _context = context;
        }

        public override async Task<TOut> Ask(TIn input, CancellationToken cancellationToken)
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
                return (TOut) (IResponse) new Error() { Message = "Транзакция отменена"};
            }
        }
    }
}