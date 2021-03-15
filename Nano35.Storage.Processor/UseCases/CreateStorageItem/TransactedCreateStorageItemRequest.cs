﻿using System;
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
        IPipelineNode<
            ICreateStorageItemRequestContract, 
            ICreateStorageItemResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateStorageItemRequestContract,
            ICreateStorageItemResultContract> _nextNode;

        public TransactedCreateStorageItemRequest(
            ApplicationContext context,
            IPipelineNode<
                ICreateStorageItemRequestContract, 
                ICreateStorageItemResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateStorageItemResultContract> Ask(
            ICreateStorageItemRequestContract input,
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
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return new CreateStorageItemTransactionErrorResult{ Message = "Товар не создан"};
            }
        }
    }
}