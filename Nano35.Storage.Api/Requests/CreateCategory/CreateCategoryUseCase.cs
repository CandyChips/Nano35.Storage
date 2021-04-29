using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryUseCase : EndPointNodeBase<ICreateCategoryRequestContract, ICreateCategoryResultContract>
    {
        private readonly IBus _bus;
        public CreateCategoryUseCase(IBus bus) => _bus = bus;
        public override async Task<ICreateCategoryResultContract> Ask(ICreateCategoryRequestContract input) => await new CreateCategoryRequest(_bus).GetResponse(input);
    }
}