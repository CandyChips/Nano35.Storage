using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleBrands
{
    public class ValidatedGetAllArticlesBrandsRequest:
        PipeNodeBase<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract>
    {
        public ValidatedGetAllArticlesBrandsRequest(
            IPipeNode<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllArticlesBrandsResultContract> Ask(
            IGetAllArticlesBrandsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}