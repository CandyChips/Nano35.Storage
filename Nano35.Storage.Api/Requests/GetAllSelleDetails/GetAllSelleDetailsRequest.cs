using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllSelleDetails
{
    public class GetAllSelleDetailsRequest :
        EndPointNodeBase<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllSelleDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllSelleDetailsResultContract> Ask(
            IGetAllSelleDetailsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllSelleDetailsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllSelleDetailsSuccessResultContract, IGetAllSelleDetailsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllSelleDetailsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllSelleDetailsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}