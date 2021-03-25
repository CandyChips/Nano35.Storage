using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class GetAllComingsRequest :
        EndPointNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllComingsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllComingsResultContract> Ask(
            IGetAllComingsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllComingsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllComingsSuccessResultContract, IGetAllComingsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllComingsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllComingsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}