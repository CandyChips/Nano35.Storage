using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class GetAllArticleCategoriesUseCase :
        UseCaseEndPointNodeBase<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticleCategoriesUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllArticlesCategoriesResultContract>> Ask(IGetAllArticlesCategoriesRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>(_bus, input)
                .GetResponse();
    }
}