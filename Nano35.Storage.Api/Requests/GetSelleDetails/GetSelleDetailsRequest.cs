﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetSelleDetails
{
    public class GetSelleDetailsByIdRequest :
        IPipelineNode<
            IGetSelleDetailsByIdRequestContract,
            IGetSelleDetailsByIdResultContract>
    {
        private readonly IBus _bus;
        public GetSelleDetailsByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetSelleDetailsByIdResultContract> Ask(
            IGetSelleDetailsByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetSelleDetailsByIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetSelleDetailsByIdSuccessResultContract, IGetSelleDetailsByIdErrorResultContract>(input);
            
            if (response.Is(out Response<IGetSelleDetailsByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetSelleDetailsByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}