using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSalle
{
    public class CreateSalleRequest :
        IPipelineNode<ICreateSalleRequestContract, ICreateSalleResultContract>
    {
        private readonly IBus _bus;
        public CreateSalleRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class CreateSalleSuccessResultContract : 
            ICreateSalleSuccessResultContract
        {
            
        }
        
        public async Task<ICreateSalleResultContract> Ask(
            ICreateSalleRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateSalleRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateSalleSuccessResultContract, ICreateSalleErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateSalleSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateSalleErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}