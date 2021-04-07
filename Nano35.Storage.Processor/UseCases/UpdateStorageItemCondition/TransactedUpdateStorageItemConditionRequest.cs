using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemCondition
{
    public class UpdateStorageItemConditionTransactionErrorResult :
        IUpdateStorageItemConditionErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemConditionRequest :
        PipeNodeBase<
            IUpdateStorageItemConditionRequestContract, 
            IUpdateStorageItemConditionResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedUpdateStorageItemConditionRequest(
            ApplicationContext context,
            IPipeNode<IUpdateStorageItemConditionRequestContract,
                IUpdateStorageItemConditionResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input,
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
                return new UpdateStorageItemConditionTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}