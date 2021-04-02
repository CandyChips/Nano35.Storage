using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class ValidatedCreateCancellationRequest:
        PipeNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        public ValidatedCreateCancellationRequest(
            IPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> next) :
            base(next) { }

        public override async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}