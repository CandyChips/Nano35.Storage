using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class ValidatedCreateSelleRequest:
        PipeNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {

        public ValidatedCreateSelleRequest(
            IPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract> next) :
            base(next) { }

        public override async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input)
        {
            return await DoNext(input);
        }
    }
}