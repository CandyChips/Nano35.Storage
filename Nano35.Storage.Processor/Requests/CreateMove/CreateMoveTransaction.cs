﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateMove
{
    public class CreateMoveTransactionErrorResult :
        ICreateMoveErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateMoveTransaction :
        IPipelineNode<
            ICreateMoveRequestContract, 
            ICreateMoveResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateMoveRequestContract,
            ICreateMoveResultContract> _nextNode;

        public CreateMoveTransaction(
            ApplicationContext context,
            IPipelineNode<
                ICreateMoveRequestContract, 
                ICreateMoveResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input,
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
                return new CreateMoveTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}