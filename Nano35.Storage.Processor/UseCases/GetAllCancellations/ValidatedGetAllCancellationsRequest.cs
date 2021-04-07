using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellations
{
    public class GetAllCancellationsValidatorErrorResult :
        IGetAllCancellationsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCancellationsRequest:
        PipeNodeBase<
            IGetAllCancellationsRequestContract, 
            IGetAllCancellationsResultContract>
    {
        public ValidatedGetAllCancellationsRequest(
            IPipeNode<IGetAllCancellationsRequestContract, 
                IGetAllCancellationsResultContract> next) : base(next)
        { }

        public override async Task<IGetAllCancellationsResultContract> Ask(
            IGetAllCancellationsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllCancellationsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}