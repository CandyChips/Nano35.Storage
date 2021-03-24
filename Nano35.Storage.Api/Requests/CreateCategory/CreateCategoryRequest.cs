using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryRequest :
        EndPointNodeBase<ICreateCategoryRequestContract, ICreateCategoryResultContract>
    {
        private readonly IBus _bus;
        
        public CreateCategoryRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<ICreateCategoryResultContract> Ask(
            ICreateCategoryRequestContract input)
        {
            var client = _bus.CreateRequestClient<ICreateCategoryRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateCategorySuccessResultContract, ICreateCategoryErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateCategorySuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateCategoryErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}