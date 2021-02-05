using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdHandler 
        : IRequestHandler<GetArticleByIdQuery, IGetArticleByIdResultContract>
    {
        private readonly IBus _bus;
        public GetArticleByIdHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<IGetArticleByIdResultContract> Handle(
            GetArticleByIdQuery message,
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IGetArticleByIdRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetArticleByIdSuccessResultContract, IGetArticleByIdErrorResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetArticleByIdSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGetArticleByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}