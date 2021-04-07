﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateMove
{
    public class CreateMoveTransactionErrorResult :
        ICreateMoveErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateMoveRequest :
        PipeNodeBase<
            ICreateMoveRequestContract, 
            ICreateMoveResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateMoveRequest(
            ApplicationContext context,
            IPipeNode<ICreateMoveRequestContract,
                ICreateMoveResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input,
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
                return new CreateMoveTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}