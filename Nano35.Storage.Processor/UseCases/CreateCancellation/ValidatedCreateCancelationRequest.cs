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
        PipeNodeBase<
            ICreateCancellationRequestContract,
            ICreateCancellationResultContract>
    {
        private readonly ApplicationContext _context;

        public ValidatedCreateCancellationRequest(
            ApplicationContext context,
            IPipeNode<ICreateCancellationRequestContract,
                ICreateCancellationResultContract> next) : base(next)
        {
            _context = context;
        }

        public override async Task<ICreateCancellationResultContract> Ask(
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
            return await DoNext(input, cancellationToken);
        }
    }
}