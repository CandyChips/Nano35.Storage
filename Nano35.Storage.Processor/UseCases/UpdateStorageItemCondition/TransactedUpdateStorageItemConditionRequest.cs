﻿using System.Threading;
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
        IPipelineNode<
            IUpdateStorageItemConditionRequestContract, 
            IUpdateStorageItemConditionResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateStorageItemConditionRequestContract,
            IUpdateStorageItemConditionResultContract> _nextNode;

        public TransactedUpdateStorageItemConditionRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateStorageItemConditionRequestContract, 
                IUpdateStorageItemConditionResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateStorageItemConditionResultContract> Ask(
            IUpdateStorageItemConditionRequestContract input,
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
                return new UpdateStorageItemConditionTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}