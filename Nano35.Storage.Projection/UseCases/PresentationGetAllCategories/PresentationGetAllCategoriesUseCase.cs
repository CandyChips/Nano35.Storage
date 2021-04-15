using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllCategories
{
    public class PresentationGetAllCategoriesUseCase :
        EndPointNodeBase<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>
    {
        private readonly IBus _bus;
        
        public PresentationGetAllCategoriesUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IPresentationGetAllCategoriesResultContract> Ask(
            IPresentationGetAllCategoriesRequestContract input) => 
            (await (new PresentationGetAllCategoriesRequest(_bus)).GetResponse(input));
    }
}