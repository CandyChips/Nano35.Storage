using System;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateArticle
{
    public class CreateArticleTransactionErrorResult :
        ICreateArticleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class TransactedCreateArticleRequest :
        PipeNodeBase<
            ICreateArticleRequestContract, 
            ICreateArticleResultContract>
    {
        private readonly ApplicationContext _context;

        public TransactedCreateArticleRequest(
            ApplicationContext context,
            IPipeNode<ICreateArticleRequestContract,
                ICreateArticleResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input,
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
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken).ConfigureAwait(false);
                return new CreateArticleTransactionErrorResult{ Message = "Наименование не создано"};
            }
        }
    }
}