using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateCategoryName
{
    public class ValidatedUpdateCategoryNameRequest:
        PipeNodeBase<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract>
    {
        public ValidatedUpdateCategoryNameRequest(
            IPipeNode<IUpdateCategoryNameRequestContract, IUpdateCategoryNameResultContract> next) :
            base(next) { }

        public override async Task<IUpdateCategoryNameResultContract> Ask(
            IUpdateCategoryNameRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}