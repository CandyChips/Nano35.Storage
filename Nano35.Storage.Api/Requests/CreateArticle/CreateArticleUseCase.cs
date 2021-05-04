using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleUseCase : UseCaseEndPointNodeBase<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly IBus _bus;
        public CreateArticleUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<ICreateArticleResultContract>> Ask(ICreateArticleRequestContract input) => 
            await new MasstransitUseCaseRequest<ICreateArticleRequestContract, ICreateArticleResultContract>(_bus, input)
                .GetResponse();
    }
}