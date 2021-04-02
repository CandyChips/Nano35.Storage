using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllComings
{
    public class ValidatedGetAllComingsRequest:
        PipeNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        public ValidatedGetAllComingsRequest(
            IPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllComingsResultContract> Ask(
            IGetAllComingsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}