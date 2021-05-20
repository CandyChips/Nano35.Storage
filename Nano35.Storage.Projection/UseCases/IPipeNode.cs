using System.Threading.Tasks;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Storage.Projection.UseCases
{
    public interface IPipeNode<in TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        Task<UseCaseResponse<TOut>> Ask(TIn input);
    }
}