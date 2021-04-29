using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.UseCases
{
    public abstract class PipeNodeBase<TIn, TOut> : 
        IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly IPipeNode<TIn, TOut> _next;
        protected PipeNodeBase(IPipeNode<TIn, TOut> next) => _next = next;
        protected Task<TOut> DoNext(TIn input, CancellationToken cancellationToken) => _next.Ask(input, cancellationToken);
        public abstract Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }
}