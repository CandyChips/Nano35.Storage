using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class GetStorageItemByIdHandler 
        : IRequestHandler<GetStorageItemByIdQuery, IGetStorageItemByIdResultContract>
    {
        private readonly IBus _bus;
        public GetStorageItemByIdHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<IGetStorageItemByIdResultContract> Handle(
            GetStorageItemByIdQuery message,
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IGetStorageItemByIdRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetStorageItemByIdSuccessResultContract, IGetStorageItemByIdErrorResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetStorageItemByIdSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGetStorageItemByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}