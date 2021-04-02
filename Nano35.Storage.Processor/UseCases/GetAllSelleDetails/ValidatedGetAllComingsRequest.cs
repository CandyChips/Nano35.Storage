using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class ValidatedGetAllSelleDetailsRequest:
        PipeNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
    {
        public ValidatedGetAllSelleDetailsRequest(
            IPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllSelleDetailsResultContract> Ask(
            IGetAllSelleDetailsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}