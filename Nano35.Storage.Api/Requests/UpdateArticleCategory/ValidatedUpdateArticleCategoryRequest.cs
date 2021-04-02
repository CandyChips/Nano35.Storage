using System.Threading.Tasks;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleCategory
{
    public class ValidatedUpdateArticleCategoryRequest:
        PipeNodeBase<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>
    {
        public ValidatedUpdateArticleCategoryRequest(
            IPipeNode<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract> next) :
            base(next) { }

        public override async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input)
        {
            return await DoNext(input);
        }
    }
}