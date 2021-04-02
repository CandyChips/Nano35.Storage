using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class ValidatedGetAllCancellationDetailsRequest:
        PipeNodeBase<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>
    {
        public ValidatedGetAllCancellationDetailsRequest(
            IPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input)
        {
            return await DoNext(input);
        }
    }
}