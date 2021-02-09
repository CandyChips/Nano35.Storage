using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateMove
{
    public class CreateMoveRequest :
        IPipelineNode<ICreateMoveRequestContract, ICreateMoveResultContract>
    {
        private readonly IBus _bus;
        public CreateMoveRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class CreateMoveSuccessResultContract : 
            ICreateMoveSuccessResultContract
        {
            
        }
        
        public async Task<ICreateMoveResultContract> Ask(
            ICreateMoveRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateMoveRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateMoveSuccessResultContract, ICreateMoveErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateMoveSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateMoveErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}