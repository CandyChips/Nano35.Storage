using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetMoveDetails
{
    public class GetMoveDetailsByIdRequest :
        IPipelineNode<
            IGetMoveDetailsByIdRequestContract,
            IGetMoveDetailsByIdResultContract>
    {
        private readonly IBus _bus;
        public GetMoveDetailsByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetMoveDetailsByIdResultContract> Ask(
            IGetMoveDetailsByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetMoveDetailsByIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetMoveDetailsByIdSuccessResultContract, IGetMoveDetailsByIdErrorResultContract>(input);
            
            if (response.Is(out Response<IGetMoveDetailsByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetMoveDetailsByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}