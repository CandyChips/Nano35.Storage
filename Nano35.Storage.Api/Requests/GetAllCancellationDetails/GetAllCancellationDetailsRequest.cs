using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsRequest :
        EndPointNodeBase<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllCancellationDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllCancellationDetailsResultContract> Ask(
            IGetAllCancellationDetailsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllCancellationDetailsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllCancellationDetailsSuccessResultContract, IGetAllCancellationDetailsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllCancellationDetailsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllCancellationDetailsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}