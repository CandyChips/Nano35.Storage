using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleUseCase :
        EndPointNodeBase<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly IBus _bus;
        
        public CreateArticleUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<ICreateArticleResultContract> Ask(
            ICreateArticleRequestContract input) => 
            (await (new CreateArticleRequest(_bus)).GetResponse(input));
    }
}