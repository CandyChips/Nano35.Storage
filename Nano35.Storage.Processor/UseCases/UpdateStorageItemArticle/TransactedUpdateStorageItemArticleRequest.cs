﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.UpdateStorageItemArticle
{
    public class UpdateStorageItemArticleTransactionErrorResult :
        IUpdateStorageItemArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedUpdateStorageItemArticleRequest :
        IPipelineNode<
            IUpdateStorageItemArticleRequestContract, 
            IUpdateStorageItemArticleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            IUpdateStorageItemArticleRequestContract,
            IUpdateStorageItemArticleResultContract> _nextNode;

        public TransactedUpdateStorageItemArticleRequest(
            ApplicationContext context,
            IPipelineNode<
                IUpdateStorageItemArticleRequestContract, 
                IUpdateStorageItemArticleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<IUpdateStorageItemArticleResultContract> Ask(
            IUpdateStorageItemArticleRequestContract input,
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
                return new UpdateStorageItemArticleTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}