using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleRequest :
        MasstransitRequest<
            ICreateArticleRequestContract,
            ICreateArticleResultContract,
            ICreateArticleSuccessResultContract,
            ICreateArticleErrorResultContract>
    {
        public CreateArticleRequest(IBus bus) : base(bus) {}
    }
}