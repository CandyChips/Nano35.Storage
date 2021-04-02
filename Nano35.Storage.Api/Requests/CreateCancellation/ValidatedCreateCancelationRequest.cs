using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class ValidatedCreateCancellationRequest :
        PipeNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        public ValidatedCreateCancellationRequest(
            IPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> next) :
            base(next) { }

        public override async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input)
        {
            return await DoNext(input);
        }
    }
}