using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetCancellationDetails
{
    public class GetCancellationDetailsRequest :
        IPipelineNode<
            IGetCancellationDetailsRequestContract,
            IGetCancellationDetailsResultContract>
    {
        private readonly IBus _bus;
        public GetCancellationDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetCancellationDetailsResultContract> Ask(
            IGetCancellationDetailsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetCancellationDetailsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetCancellationDetailsSuccessResultContract, IGetCancellationDetailsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetCancellationDetailsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetCancellationDetailsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}