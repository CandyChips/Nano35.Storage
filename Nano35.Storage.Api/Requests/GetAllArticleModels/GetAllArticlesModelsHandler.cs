using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelsHandler 
        : IRequestHandler<GetAllArticlesModelsQuery, IGetAllArticlesModelsResultContract>
    {
        private readonly IBus _bus;
        public GetAllArticlesModelsHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<IGetAllArticlesModelsResultContract> Handle(
            GetAllArticlesModelsQuery message,
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IGetAllArticlesModelsRequestContract>(TimeSpan.FromSeconds(10));
                
            var response = await client
                .GetResponse<IGetAllArticlesModelsSuccessResultContract, IGetAllArticlesModelsErrorResultContract>(message, cancellationToken);

            if (response.Is(out Response<IGetAllArticlesModelsSuccessResultContract> successResponse))
                return successResponse.Message;
                
            if (response.Is(out Response<IGetAllArticlesModelsErrorResultContract> errorResponse))
                return errorResponse.Message;
                
            throw new InvalidOperationException();
        }
    }
}