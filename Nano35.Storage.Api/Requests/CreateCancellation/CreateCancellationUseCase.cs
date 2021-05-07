using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class CreateCancellationUseCase : UseCaseEndPointNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly IBus _bus;
        public CreateCancellationUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<ICreateCancellationResultContract>> Ask(
            ICreateCancellationRequestContract input)
        {
            
            if (input.NewId == Guid.Empty) return Pass("Обновите страницу и попробуйте еще раз");
            if (input.InstanceId == Guid.Empty) return Pass("Обновите страницу и попробуйте еще раз");
            if (!input.Details.Any()) return Pass("Нет деталей списания");
            if (input.UnitId == Guid.Empty) return Pass("Обновите страницу и попробуйте еще раз");
            
            return await new MasstransitUseCaseRequest<ICreateCancellationRequestContract, ICreateCancellationResultContract>(
                    _bus, input)
                .GetResponse();
        }
    }
}