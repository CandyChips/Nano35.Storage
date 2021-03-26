using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateCategoryParentCategoryId
{
    public class UpdateCategoryParentCategoryIdRequest :
        MasstransitRequest
        <IUpdateCategoryParentCategoryIdRequestContract,
            IUpdateCategoryParentCategoryIdResultContract,
            IUpdateCategoryParentCategoryIdSuccessResultContract,
            IUpdateCategoryParentCategoryIdErrorResultContract>
    {
        public UpdateCategoryParentCategoryIdRequest(IBus bus) : base(bus) {}
    }
}