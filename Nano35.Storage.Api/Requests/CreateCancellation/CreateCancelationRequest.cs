using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class CreateCancellationRequest :
        EndPointNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly IBus _bus;
        
        public CreateCancellationRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateCancellationRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateCancellationSuccessResultContract, ICreateCancellationErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateCancellationSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateCancellationErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}