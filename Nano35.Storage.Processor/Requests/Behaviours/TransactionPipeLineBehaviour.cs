using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.Behaviours
{
    public class TransactionPipeLineBehaviour<TIn, TOut> :
        IPipelineBehavior<TIn, TOut>
        where TIn : ICommandRequest<TOut>
    {
        private readonly ILogger<TransactionPipeLineBehaviour<TIn, TOut>> _logger;
        private readonly ApplicationContext _context;

        public TransactionPipeLineBehaviour(
            ILogger<TransactionPipeLineBehaviour<TIn, TOut>> logger, 
            ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            
            var response = await next();
            
            switch (response)
            {
                case IError:
                    await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                    _logger.LogError("Transaction refused");
                    break;
                case ISuccess:
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    break;
            }

            return response;
        }
    }
}