using System.Threading;
using System.Threading.Tasks;

namespace Nano35.Storage.Processor.UseCases
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }
}