using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class GetAllArticleModelsUseCase :
        UseCaseEndPointNodeBase<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticleModelsUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllArticlesModelsResultContract>> Ask(IGetAllArticlesModelsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>(_bus, input)
                .GetResponse();
    }
}