using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleBrand
{
    public class ValidatedUpdateArticleBrandRequest:
        PipeNodeBase<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>
    {
        public ValidatedUpdateArticleBrandRequest(
            IPipeNode<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract> next) :
            base(next) { }

        public override async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}