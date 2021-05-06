using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class UpdateArticleInfoUseCase :
        UseCaseEndPointNodeBase<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateArticleInfoUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateArticleInfoResultContract>> Ask(IUpdateArticleInfoRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>(_bus, input)
                .GetResponse();
    }
}