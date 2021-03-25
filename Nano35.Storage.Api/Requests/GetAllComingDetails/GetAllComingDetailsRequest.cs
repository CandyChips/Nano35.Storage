using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllComingDetails
{
    public class GetAllComingDetailsRequest :
        EndPointNodeBase<IGetAllComingDetailsRequestContract, IGetAllComingDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllComingDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllComingDetailsResultContract> Ask(
            IGetAllComingDetailsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllComingDetailsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllComingDetailsSuccessResultContract, IGetAllComingDetailsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllComingDetailsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllComingDetailsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}