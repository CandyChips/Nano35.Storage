using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class CreateCancellationValidatorErrorResult : 
        ICreateCancellationErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCancellationRequest:
        IPipelineNode<
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateCancellationRequestContract, 
            ICreateCancellationResultContract> _nextNode;

        public ValidatedCreateCancellationRequest(
            ApplicationContext context,
            IPipelineNode<
                ICreateCancellationRequestContract,
                ICreateCancellationResultContract> nextNode)
        {
            _context = context;
            _nextNode = nextNode;
        }

        public async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            var count = _context
                .Warehouses
                .FirstOrDefaultAsync(a =>
                        a.StorageItemId == input.Details.FirstOrDefault().StorageItemId,
                    cancellationToken: cancellationToken)
                .Result
                .Count;
            
            if (input.Details.FirstOrDefault().Count > count)
            {
                return new CreateCancellationValidatorErrorResult() {Message = "евозможно списать больше чем есть в ячейке"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}