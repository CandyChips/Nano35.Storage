using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleCategory
{
    public class UpdateArticleCategoryUseCase :
        UseCaseEndPointNodeBase<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>
    {
        private readonly IBus _bus;
        public UpdateArticleCategoryUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<UseCaseResponse<IUpdateArticleCategoryResultContract>> Ask(IUpdateArticleCategoryRequestContract input) => 
            await new MasstransitUseCaseRequest<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>(_bus, input)
                .GetResponse();
    }
}