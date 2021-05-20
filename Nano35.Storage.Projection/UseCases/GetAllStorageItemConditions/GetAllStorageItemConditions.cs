using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.GetAllStorageItemConditions
{
    public class GetAllStorageItemConditions :
        EndPointNodeBase<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>
    {
        private readonly IBus _bus;
        public GetAllStorageItemConditions(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllStorageItemConditionsResultContract>> Ask(IGetAllStorageItemConditionsRequestContract input) =>
            await new MasstransitRequest<IGetAllStorageItemConditionsRequestContract, IGetAllStorageItemConditionsResultContract>(_bus, input)
                .GetResponse();
    }   
}