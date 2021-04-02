using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.UseCases
{
    public abstract class EndPointNodeBase<TIn, TOut> : 
        IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        public abstract Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }
}