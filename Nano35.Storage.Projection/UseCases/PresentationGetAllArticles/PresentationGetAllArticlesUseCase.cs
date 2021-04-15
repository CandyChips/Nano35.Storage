using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Projection.UseCases.PresentationGetAllArticles
{
    public class PresentationGetAllArticlesUseCase :
        EndPointNodeBase<IPresentationGetAllArticlesRequestContract, IPresentationGetAllArticlesResultContract>
    {
        private readonly IBus _bus;
        
        public PresentationGetAllArticlesUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IPresentationGetAllArticlesResultContract> Ask(
            IPresentationGetAllArticlesRequestContract input) => 
            (await (new PresentationGetAllArticlesRequest(_bus)).GetResponse(input));
    }
}