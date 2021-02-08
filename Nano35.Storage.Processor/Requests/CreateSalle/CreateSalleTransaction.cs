using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests.CreateSalle
{
    public class CreateSalleTransactionErrorResult :
        ICreateSalleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateSalleTransaction :
        IPipelineNode<
            ICreateSalleRequestContract, 
            ICreateSalleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateSalleRequestContract,
            ICreateSalleResultContract> _nextNode;

        public CreateSalleTransaction(
            ApplicationContext context,
            IPipelineNode<
                ICreateSalleRequestContract, 
                ICreateSalleResultContract> nextNode)
        {
            _nextNode = nextNode;
            _context = context;
        }

        public async Task<ICreateSalleResultContract> Ask(
            ICreateSalleRequestContract input,
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
                return new CreateSalleTransactionErrorResult{ Message = "Транзакция отменена"};
            }
        }
    }
}