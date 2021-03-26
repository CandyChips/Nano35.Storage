using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdUseCase :
        EndPointNodeBase<IGetArticleByIdRequestContract, IGetArticleByIdResultContract>
    {
        private readonly IBus _bus;
        
        public GetArticleByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetArticleByIdResultContract> Ask(
            IGetArticleByIdRequestContract input) => 
            (await (new GetArticleByIdRequest(_bus)).GetResponse(input));
    }
}