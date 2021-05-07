using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryUseCase : UseCaseEndPointNodeBase<ICreateCategoryRequestContract, ICreateCategoryResultContract>
    {
        private readonly IBus _bus;
        public CreateCategoryUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<ICreateCategoryResultContract>> Ask(ICreateCategoryRequestContract input)
        {
            
            if (input.NewId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");
            if (input.InstanceId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");

            return await new MasstransitUseCaseRequest<ICreateCategoryRequestContract, ICreateCategoryResultContract>(_bus,
                    input)
                .GetResponse();
        }
    }
}