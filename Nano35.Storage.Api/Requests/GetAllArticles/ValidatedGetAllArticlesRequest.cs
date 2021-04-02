using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class ValidatedGetAllArticlesRequest:
        PipeNodeBase<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>
    {
        public ValidatedGetAllArticlesRequest(
            IPipeNode<IGetAllArticlesRequestContract, IGetAllArticlesResultContract> next) :
            base(next) { }

        public override async Task<IGetAllArticlesResultContract> Ask(
            IGetAllArticlesRequestContract input)
        {
            return await DoNext(input);
        }
    }
}