using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllMoves
{
    public class GetAllMovesRequest :
        IPipelineNode<
            IGetAllMovesRequestContract,
            IGetAllMovesResultContract>
    {
        private readonly IBus _bus;
        public GetAllMovesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllMovesResultContract> Ask(
            IGetAllMovesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllMovesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllMovesSuccessResultContract, IGetAllMovesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllMovesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllMovesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}