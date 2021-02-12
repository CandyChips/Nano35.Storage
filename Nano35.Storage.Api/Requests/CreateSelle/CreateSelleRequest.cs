using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class CreateSelleRequest :
        IPipelineNode<
            ICreateSelleRequestContract,
            ICreateSelleResultContract>
    {
        private readonly IBus _bus;
        public CreateSelleRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateSelleRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateSelleSuccessResultContract, ICreateSelleErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateSelleSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateSelleErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}