﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class GetStorageItemByIdRequest :
        IPipelineNode<IGetStorageItemByIdRequestContract, IGetStorageItemByIdResultContract>
    {
        private readonly IBus _bus;
        public GetStorageItemByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        private class GetStorageItemByIdSuccessResultContract : 
            IGetStorageItemByIdSuccessResultContract
        {
            public IStorageItemViewModel Data { get; set; }
        }
        
        public async Task<IGetStorageItemByIdResultContract> Ask(
            IGetStorageItemByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetStorageItemByIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetStorageItemByIdSuccessResultContract, IGetStorageItemByIdErrorResultContract>(input);
            
            if (response.Is(out Response<IGetStorageItemByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetStorageItemByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}