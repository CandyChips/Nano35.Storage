using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdUseCase :
        UseCaseEndPointNodeBase<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>
    {
        private readonly IBus _bus;
        
        public GetArticleByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        public override async Task<UseCaseResponse<IGetArticleByIdResultContract>> Ask(IGetArticleByIdRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>(_bus, input)
                .GetResponse();
    }
}