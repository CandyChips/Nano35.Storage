using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class GetAllArticleBrandsHandler 
        : IRequestHandler<GetAllArticleBrandsQuery, IGetAllArticlesBrandsResultContract>
    {
        private readonly IBus _bus;
        public GetAllArticleBrandsHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<IGetAllArticlesBrandsResultContract> Handle(
            GetAllArticleBrandsQuery message,
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IGetAllArticlesBrandsRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetAllArticlesBrandsSuccessResultContract, IGetAllArticlesBrandsErrorResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetAllArticlesBrandsSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGetAllArticlesBrandsErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}