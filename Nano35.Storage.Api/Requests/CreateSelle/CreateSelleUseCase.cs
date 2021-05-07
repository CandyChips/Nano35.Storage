using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class CreateSelleUseCase : UseCaseEndPointNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {
        private readonly IBus _bus;
        public CreateSelleUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<ICreateSelleResultContract>> Ask(ICreateSelleRequestContract input)
        {
            
            if (!input.Details.Any())
                return Pass("Нет деталей продажи");
            if (input.InstanceId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");
            if (input.NewId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");

            return await new MasstransitUseCaseRequest<ICreateSelleRequestContract, ICreateSelleResultContract>(_bus,
                    input)
                .GetResponse();
        }
    }
}