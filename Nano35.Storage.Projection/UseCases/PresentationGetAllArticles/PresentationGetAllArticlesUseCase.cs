using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllArticles
{
    public class PresentationGetAllArticles :
        EndPointNodeBase<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>
    {
        private readonly IBus _bus;
        public PresentationGetAllArticles(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IPresentationGetAllArticlesResultContract>> Ask(IPresentationGetAllArticlesRequestContract input) =>
            await new MasstransitRequest<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>(_bus, input)
                .GetResponse();
    }   
}