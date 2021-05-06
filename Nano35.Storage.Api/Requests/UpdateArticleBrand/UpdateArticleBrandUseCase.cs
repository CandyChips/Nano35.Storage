using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleBrand
{
    public class UpdateArticleBrandUseCase :
        UseCaseEndPointNodeBase<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateArticleBrandUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateArticleBrandResultContract>> Ask(IUpdateArticleBrandRequestContract input) =>
            await new MasstransitUseCaseRequest<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>(_bus, input)
                .GetResponse();
    }
}