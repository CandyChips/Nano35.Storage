using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateStorageItem
{
    public class CreateStorageItemTransactionErrorResult :
        ICreateStorageItemErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateStorageItemRequest :
        PipeNodeBase<
            ICreateStorageItemRequestContract, 
            ICreateStorageItemResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateStorageItemRequest(
            ApplicationContext context,
            IPipeNode<ICreateStorageItemRequestContract,
                ICreateStorageItemResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input,
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
                return new CreateStorageItemTransactionErrorResult{ Message = "Товар не создан"};
            }
        }
    }
}