using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.UpdateArticleInfo
{
    public class ValidatedUpdateArticleInfoRequest:
        PipeNodeBase<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>
    {
        public ValidatedUpdateArticleInfoRequest(
            IPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract> next) :
            base(next) { }

        public override async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}