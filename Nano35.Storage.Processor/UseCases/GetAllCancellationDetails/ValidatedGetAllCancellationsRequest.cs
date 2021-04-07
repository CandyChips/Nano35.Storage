using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsValidatorErrorResult :
        IGetAllCancellationDetailsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCancellationDetailsRequest:
        PipeNodeBase<
            IGetAllCancellationDetailsRequestContract, 
            IGetAllCancellationDetailsResultContract>
    {
        public ValidatedGetAllCancellationDetailsRequest(
            IPipeNode<IGetAllCancellationDetailsRequestContract,
                IGetAllCancellationDetailsResultContract> next) : base(next)
        { }

        public override async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}