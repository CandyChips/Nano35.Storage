using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Storage.Projection.UseCases
{
    public abstract class EndPointNodeBase<TIn, TOut> : IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input);
    }
}