using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateComing
{
    public class CreateComingUseCase : UseCaseEndPointNodeBase<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly IBus _bus;
        public CreateComingUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<ICreateComingResultContract>> Ask(ICreateComingRequestContract input)
        {
            
            if (!input.Details.Any())
                return Pass("Нет деталей оприходования");
            if (input.ClientId == Guid.Empty)
                return Pass("Не выбран клиент");
            if (input.InstanceId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");
            if (input.NewId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");

            return await new MasstransitUseCaseRequest<ICreateComingRequestContract, ICreateComingResultContract>(_bus, input)
                .GetResponse();
        }  
    }
}