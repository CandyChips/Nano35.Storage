using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class ValidatedCreateSelleRequest:
        PipeNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {
        public ValidatedCreateSelleRequest(
            IPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract> next) :
            base(next) { }

        public override async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}