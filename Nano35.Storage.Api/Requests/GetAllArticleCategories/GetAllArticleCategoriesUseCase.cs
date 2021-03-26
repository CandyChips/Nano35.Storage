using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class GetAllArticleCategoriesUseCase :
        EndPointNodeBase<IGetAllArticlesCategoriesRequestContract, IGetAllArticlesCategoriesResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticleCategoriesUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input) => 
            (await (new GetAllArticleCategoriesRequest(_bus)).GetResponse(input));
    }
}