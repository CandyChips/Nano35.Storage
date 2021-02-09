using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancelation
{
    public class CreateCancelationRequest :
        IPipelineNode<ICreateCancelationRequestContract, ICreateCancelationResultContract>
    {
        private readonly IBus _bus;
        public CreateCancelationRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class CreateCancelationSuccessResultContract : 
            ICreateCancelationSuccessResultContract
        {
            
        }
        
        public async Task<ICreateCancelationResultContract> Ask(
            ICreateCancelationRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateCancelationRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateCancelationSuccessResultContract, ICreateCancelationErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateCancelationSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateCancelationErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}