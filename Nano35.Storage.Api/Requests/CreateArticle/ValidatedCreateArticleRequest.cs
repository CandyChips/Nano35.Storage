using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class ValidatedCreateArticleRequest:
        PipeNodeBase<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        public ValidatedCreateArticleRequest(
            IPipeNode<ICreateArticleRequestContract, ICreateArticleResultContract> next) :
            base(next) { }

        public override async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input)
        {
            return await DoNext(input);
        }
    }
}