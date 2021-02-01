﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests
{
    public class GetAllArticlesQuery : 
        IGetAllArticlesRequestContract, 
        IQueryRequest<IGetAllArticlesResultContract>
    {
        public Guid InstanceId { get; set; }
        
        public class GetAllClientsHandler 
            : IRequestHandler<GetAllArticlesQuery, IGetAllArticlesResultContract>
        {
            private readonly IBus _bus;
            public GetAllClientsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllArticlesResultContract> Handle(
                GetAllArticlesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllArticlesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllArticlesSuccessResultContract, IGetAllArticlesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllArticlesSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllArticlesErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}