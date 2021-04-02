using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetArticleById
{
    public class ValidatedGetArticleByIdRequest:
        PipeNodeBase<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>
    {
        public ValidatedGetArticleByIdRequest(
            IPipeNode<IGetArticleByIdRequestContract, IGetArticleByIdResultContract> next) :
            base(next) { }

        public override async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}