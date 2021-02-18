using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetMoveDetails
{
    public class GetMoveDetailsRequest :
        IPipelineNode<
            IGetMoveDetailsRequestContract,
            IGetMoveDetailsResultContract>
    {
        private readonly IBus _bus;
        public GetMoveDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetMoveDetailsResultContract> Ask(
            IGetMoveDetailsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetMoveDetailsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetMoveDetailsSuccessResultContract, IGetMoveDetailsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetMoveDetailsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetMoveDetailsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}