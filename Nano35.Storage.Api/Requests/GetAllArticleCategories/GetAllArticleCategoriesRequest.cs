using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesRequest :
        IPipelineNode<
            IGetAllArticlesCategoriesRequestContract,
            IGetAllArticlesCategoriesResultContract>
    {
        private readonly IBus _bus;
        public GetAllArticlesCategoriesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllArticlesCategoriesResultContract> Ask(
            IGetAllArticlesCategoriesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllArticlesCategoriesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllArticlesCategoriesSuccessResultContract, IGetAllArticlesCategoriesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllArticlesCategoriesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllArticlesCategoriesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}