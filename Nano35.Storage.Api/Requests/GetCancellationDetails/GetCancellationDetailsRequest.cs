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
            IGetCancellationDetailsByIdRequestContract,
            IGetCancellationDetailsByIdResultContract>
    {
        private readonly IBus _bus;
        public GetCancellationDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetCancellationDetailsByIdResultContract> Ask(
            IGetCancellationDetailsByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetCancellationDetailsByIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetCancellationDetailsByIdSuccessResultContract, IGetCancellationDetailsByIdErrorResultContract>(input);
            
            if (response.Is(out Response<IGetCancellationDetailsByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetCancellationDetailsByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}