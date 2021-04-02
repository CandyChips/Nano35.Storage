using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleCategories
{
    public class ValidatedGetAllArticlesCategoriesRequest:
        PipeNodeBase<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>
    {
        public ValidatedGetAllArticlesCategoriesRequest(
            IPipeNode<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract> next) :
            base(next) { }

        public override async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}