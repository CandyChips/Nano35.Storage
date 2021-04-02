using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleModels
{
    public class ValidatedGetAllArticlesModelsRequest:
        PipeNodeBase<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        public ValidatedGetAllArticlesModelsRequest(
            IPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract> next) :
            base(next) { }

        public override async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input,
            CancellationToken cancellationToken)
        {
            return await DoNext(input, cancellationToken);
        }
    }
}