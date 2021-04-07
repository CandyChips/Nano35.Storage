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
        PipeNodeBase<
            ICreateSelleRequestContract,
            ICreateSelleResultContract>
    {
        private readonly ApplicationContext _context;

        public ValidatedCreateSelleRequest(
            ApplicationContext context,
            IPipeNode<ICreateSelleRequestContract,
                ICreateSelleResultContract> next) : base(next)
        { 
            _context = context;
        }

        public override async Task<ICreateSelleResultContract> Ask(
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
            return await DoNext(input, cancellationToken);
        }
    }
}