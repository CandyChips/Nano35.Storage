using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class GetAllArticlesUseCase :
        EndPointNodeBase<IGetAllArticlesRequestContract, IGetAllArticlesResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticlesUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllArticlesResultContract> Ask(
            IGetAllArticlesRequestContract input) => 
            (await (new GetAllArticlesRequest(_bus)).GetResponse(input));
    }
}