using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class ValidatedUpdateArticleInfoRequest:
        PipeNodeBase<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>
    {
        public ValidatedUpdateArticleInfoRequest(
            IPipeNode<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract> next) :
            base(next) { }

        public override async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input)
        {
            return await DoNext(input);
        }
    }
}