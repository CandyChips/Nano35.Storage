using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class GetAllArticlesBrandsRequest :
        EndPointNodeBase<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticlesBrandsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllArticlesBrandsResultContract> Ask(
            IGetAllArticlesBrandsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllArticlesBrandsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllArticlesBrandsSuccessResultContract, IGetAllArticlesBrandsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllArticlesBrandsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllArticlesBrandsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}