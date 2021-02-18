using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.GetAllWarehousesOfUnit
{
    public class GetAllWarehousesOfUnitRequest :
        IPipelineNode<
            IGetAllWarehousesOfUnitRequestContract,
            IGetAllWarehousesOfUnitResultContract>
    {
        private readonly IBus _bus;
        public GetAllWarehousesOfUnitRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllWarehousesOfUnitResultContract> Ask(
            IGetAllWarehousesOfUnitRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllWarehousesOfUnitRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllWarehousesOfUnitSuccessResultContract, IGetAllWarehousesOfUnitErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllWarehousesOfUnitSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllWarehousesOfUnitErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}