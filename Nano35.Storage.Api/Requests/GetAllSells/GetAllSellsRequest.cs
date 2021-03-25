using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSells
{
    public class GetAllSellsRequest :
        EndPointNodeBase<IGetAllSellsRequestContract, IGetAllSellsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllSellsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllSellsResultContract> Ask(
            IGetAllSellsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllSellsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllSellsSuccessResultContract, IGetAllSellsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllSellsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllSellsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}