using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllArticlesModels
{
    public class GetAllArticlesModelsRequest :
        IPipelineNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        private readonly IBus _bus;
        public GetAllArticlesModelsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class GetAllArticlesModelsSuccessResultContract : 
            IGetAllArticlesModelsSuccessResultContract
        {
            public IEnumerable<string> Data { get; set; }
        }
        
        public async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllArticlesModelsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllArticlesModelsSuccessResultContract, IGetAllArticlesModelsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllArticlesModelsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllArticlesModelsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}