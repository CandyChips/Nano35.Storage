using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllCategories
{
    public class PresentationGetAllCategories :
        EndPointNodeBase<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>
    {
        private readonly IBus _bus;
        public PresentationGetAllCategories(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IPresentationGetAllCategoriesResultContract>> Ask(IPresentationGetAllCategoriesRequestContract input) =>
            await new MasstransitRequest<IPresentationGetAllCategoriesRequestContract, IPresentationGetAllCategoriesResultContract>(_bus, input)
                .GetResponse();
    }   
}