using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryRequest :
        MasstransitRequest<
            ICreateCategoryRequestContract,
            ICreateCategoryResultContract,
            ICreateCategorySuccessResultContract,
            ICreateCategoryErrorResultContract>
    {
        public CreateCategoryRequest(IBus bus) : base(bus) {}
    }
}