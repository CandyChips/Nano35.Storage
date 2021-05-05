using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CheckExistArticle
{
    public class CheckExistArticleUseCase : UseCaseEndPointNodeBase<ICheckExistArticleRequestContract, ICheckExistArticleResultContract>
    {
        private readonly IBus _bus;
        public CheckExistArticleUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<ICheckExistArticleResultContract>> Ask(ICheckExistArticleRequestContract input) => 
            await new MasstransitUseCaseRequest<ICheckExistArticleRequestContract, ICheckExistArticleResultContract>(_bus, input)
                .GetResponse();
    }
}