using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CreateMoveUseCase : UseCaseEndPointNodeBase<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly IBus _bus;
        public CreateMoveUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<ICreateMoveResultContract>> Ask(ICreateMoveRequestContract input)
        {
            
            if (!input.Details.Any())
                return Pass("Нет деталей перемещения");
            if (input.InstanceId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");
            if (input.NewId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");

            return await new MasstransitUseCaseRequest<ICreateMoveRequestContract, ICreateMoveResultContract>(_bus, input)
                .GetResponse();
        }
    }
}