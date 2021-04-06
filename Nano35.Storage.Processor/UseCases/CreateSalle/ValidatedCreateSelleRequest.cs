using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class CreateSelleValidatorErrorResult : 
        ICreateSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateSelleRequest:
        IPipelineNode<
            ICreateSelleRequestContract,
            ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IPipelineNode<
            ICreateSelleRequestContract, 
            ICreateSelleResultContract> _nextNode;

        public ValidatedCreateSelleRequest(
            ApplicationContext context,
            IPipelineNode<
                ICreateSelleRequestContract,
                ICreateSelleResultContract> nextNode)
        { 
            _context = context;
            _nextNode = nextNode;
        }

        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input,
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
                return new CreateSelleValidatorErrorResult() {Message = "Невозможно списать больше чем есть в ячейке"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}