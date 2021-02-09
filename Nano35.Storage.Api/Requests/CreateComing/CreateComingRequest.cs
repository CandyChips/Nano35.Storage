using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateComing
{
    public class CreateComingRequest :
        IPipelineNode<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly IBus _bus;
        public CreateComingRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class CreateComingSuccessResultContract : 
            ICreateComingSuccessResultContract
        {
            
        }
        
        public async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateComingRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateComingSuccessResultContract, ICreateComingErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateComingSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateComingErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}