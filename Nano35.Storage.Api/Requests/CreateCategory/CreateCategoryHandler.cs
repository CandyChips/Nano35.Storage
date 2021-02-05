using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryHandler : 
        IRequestHandler<CreateCategoryCommand, ICreateCategoryResultContract>
    {
        private readonly IBus _bus;
        public CreateCategoryHandler(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<ICreateCategoryResultContract> Handle(
            CreateCategoryCommand message, 
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<ICreateCategoryRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateCategorySuccessResultContract, ICreateCategoryErrorResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<ICreateCategorySuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateCategoryErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}