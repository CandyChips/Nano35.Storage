using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.UpdateCategoryName
{
    public class UpdateCategoryNameRequest : 
        MasstransitRequest
        <IUpdateCategoryNameRequestContract,
            IUpdateCategoryNameResultContract,
            IUpdateCategoryNameSuccessResultContract,
            IUpdateCategoryNameErrorResultContract>
    {
        public UpdateCategoryNameRequest(IBus bus) : base(bus) {}
    }
}