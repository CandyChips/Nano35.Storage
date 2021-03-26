using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllMoveDetails
{
    public class GetAllMoveDetailsRequest :
        EndPointNodeBase<IGetAllMoveDetailsRequestContract, IGetAllMoveDetailsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllMoveDetailsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllMoveDetailsResultContract> Ask(
            IGetAllMoveDetailsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllMoveDetailsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllMoveDetailsSuccessResultContract, IGetAllMoveDetailsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllMoveDetailsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllMoveDetailsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}