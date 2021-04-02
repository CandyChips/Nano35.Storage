using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellations
{
    public class ValidatedGetAllCancellationsRequest:
        PipeNodeBase<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>
    {
        public ValidatedGetAllCancellationsRequest(
            IPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllCancellationsResultContract> Ask(
            IGetAllCancellationsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}