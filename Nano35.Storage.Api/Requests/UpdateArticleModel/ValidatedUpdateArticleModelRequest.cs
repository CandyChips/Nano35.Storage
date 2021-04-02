using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleModel
{
    public class ValidatedUpdateArticleModelRequest:
        PipeNodeBase<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract>
    {
        public ValidatedUpdateArticleModelRequest(
            IPipeNode<IUpdateArticleModelRequestContract, IUpdateArticleModelResultContract> next) :
            base(next) { }

        public override async Task<IUpdateArticleModelResultContract> Ask(
            IUpdateArticleModelRequestContract input)
        {
            return await DoNext(input);
        }
    }
}