using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateComing
{
    public class CreateComingTransactionErrorResult :
        ICreateComingErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateComingRequest :
        PipeNodeBase<
            ICreateComingRequestContract, 
            ICreateComingResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateComingRequest(
            ApplicationContext context,
            IPipeNode<ICreateComingRequestContract,
                ICreateComingResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input,
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
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return new CreateComingTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}