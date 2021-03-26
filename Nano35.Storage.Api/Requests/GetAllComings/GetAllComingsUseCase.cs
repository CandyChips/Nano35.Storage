using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class GetAllComingsUseCase :
        EndPointNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllComingsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllComingsResultContract> Ask(
            IGetAllComingsRequestContract input) => 
            (await (new GetAllComingsRequest(_bus)).GetResponse(input));
    }
}