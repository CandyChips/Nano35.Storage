using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class GetAllArticlesUseCase :
        UseCaseEndPointNodeBase<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>
    {
        private readonly IBus _bus;
        public GetAllArticlesUseCase(IBus bus) { _bus = bus; }

        public override async Task<UseCaseResponse<IGetAllArticlesResultContract>> Ask(IGetAllArticlesRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>(_bus, input)
                .GetResponse();
    }
}