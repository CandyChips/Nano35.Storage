using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Repair.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllStorageItems
{
    public class PresentationGetAllStorageItems :
        EndPointNodeBase<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>
    {
        private readonly IBus _bus;
        public PresentationGetAllStorageItems(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IPresentationGetAllStorageItemsResultContract>> Ask(IPresentationGetAllStorageItemsRequestContract input) =>
            await new MasstransitRequest<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>(_bus, input)
                .GetResponse();
    }   
}