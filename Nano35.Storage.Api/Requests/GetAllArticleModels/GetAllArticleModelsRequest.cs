using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelsUseCase :
        EndPointNodeBase<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticlesModelsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input) => 
            (await (new GetAllArticlesModelsRequest(_bus)).GetResponse(input));
    }
}