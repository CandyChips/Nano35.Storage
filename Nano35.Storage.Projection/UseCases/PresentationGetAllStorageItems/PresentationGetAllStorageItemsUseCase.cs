using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllStorageItems
{
    public class PresentationGetAllStorageItemsUseCase :
        EndPointNodeBase<IPresentationGetAllStorageItemsRequestContract, IPresentationGetAllStorageItemsResultContract>
    {
        private readonly IBus _bus;
        
        public PresentationGetAllStorageItemsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IPresentationGetAllStorageItemsResultContract> Ask(
            IPresentationGetAllStorageItemsRequestContract input) => 
            (await (new PresentationGetAllStorageItemsRequest(_bus)).GetResponse(input));
    }
}