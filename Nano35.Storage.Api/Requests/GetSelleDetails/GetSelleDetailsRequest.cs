using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetSelleDetails
{
    public class GetSelleDetailsRequest :
        IPipelineNode<
            IGetSelleDetailsRequestContract,
            IGetSelleDetailsResultContract>
    {
        private readonly IBus _bus;
        public GetSelleDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetSelleDetailsResultContract> Ask(
            IGetSelleDetailsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetSelleDetailsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetSelleDetailsSuccessResultContract, IGetSelleDetailsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetSelleDetailsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetSelleDetailsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}