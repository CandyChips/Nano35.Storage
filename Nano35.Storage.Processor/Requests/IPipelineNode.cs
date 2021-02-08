using System.Threading.Tasks;

namespace Nano35.Storage.Processor.Requests
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input);
    }
}