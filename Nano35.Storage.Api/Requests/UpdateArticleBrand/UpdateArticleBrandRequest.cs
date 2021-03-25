using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleBrand
{
    public class UpdateArticleBrandRequest :
        EndPointNodeBase<IUpdateArticleBrandRequestContract, IUpdateArticleBrandResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateArticleBrandRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateArticleBrandResultContract> Ask(
            IUpdateArticleBrandRequestContract input)
        {
            var client = _bus.CreateRequestClient<IUpdateArticleBrandRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IUpdateArticleBrandSuccessResultContract, IUpdateArticleBrandErrorResultContract>(input);
            
            if (response.Is(out Response<IUpdateArticleBrandSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateArticleBrandErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}