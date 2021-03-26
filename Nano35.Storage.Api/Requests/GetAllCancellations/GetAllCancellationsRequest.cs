using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllCancellations
{
    public class GetAllCancellationsRequest :
        EndPointNodeBase<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllCancellationsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllCancellationsResultContract> Ask(
            IGetAllCancellationsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllCancellationsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllCancellationsSuccessResultContract, IGetAllCancellationsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllCancellationsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllCancellationsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}