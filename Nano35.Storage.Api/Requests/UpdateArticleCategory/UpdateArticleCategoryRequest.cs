using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateArticleCategory
{
    public class UpdateArticleCategoryRequest : 
        MasstransitRequest
        <IUpdateArticleCategoryRequestContract,
            IUpdateArticleCategoryResultContract,
            IUpdateArticleCategorySuccessResultContract,
            IUpdateArticleCategoryErrorResultContract>
    {
        public UpdateArticleCategoryRequest(IBus bus) : base(bus) {}
    }
}