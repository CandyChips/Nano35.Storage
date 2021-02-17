using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Models;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetComingDetailsById
{
    public class GetComingDetailsByIdRequest :
        IPipelineNode<
            IGetComingDetailsByIdRequestContract,
            IGetComingDetailsByIdResultContract>
    {
        private readonly IBus _bus;

        public GetComingDetailsByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetComingDetailsByIdResultContract> Ask
            (IGetComingDetailsByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetComingDetailsByIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetComingDetailsByIdSuccessResultContract, IGetComingDetailsByIdErrorResultContract>(input);
            
            if (response.Is(out Response<IGetComingDetailsByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetComingDetailsByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }   
}