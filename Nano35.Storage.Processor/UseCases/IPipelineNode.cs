using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Storage.Processor.UseCases
{
    public interface IPipelineNode<TIn, TOut>
    {
        Task<TOut> Ask(TIn input, CancellationToken cancellationToken);
    }

    // Contract request reduction
    // TMessage -> TResponse => ( TSuccess / TError )
    public abstract class MasstransitRequest<TMessage, TResponse, TSuccess, TError> 
        where TMessage : class, IRequest
        where TResponse : class, IResponse
        where TSuccess : class, ISuccess, TResponse
        where TError : class, IError, TResponse
    {
        private readonly IRequestClient<TMessage> _requestClient;
        private readonly TMessage _request;

        protected MasstransitRequest(IBus bus, TMessage request)
        {
            _requestClient = bus.CreateRequestClient<TMessage>(TimeSpan.FromSeconds(10));
            _request = request;
        }

        public async Task<TResponse> GetResponse()
        {
            var responseGetClientString = await _requestClient.GetResponse<TSuccess, TError>(_request);
            if (responseGetClientString.Is(out Response<TSuccess> successResponse))
                return successResponse.Message;
            if (responseGetClientString.Is(out Response<TError> errorResponse))
                return errorResponse.Message;
            throw new Exception();
        }
    }

    public class GetClientStringById : 
        MasstransitRequest
        <IGetClientStringByIdRequestContract, 
         IGetClientStringByIdResultContract,
         IGetClientStringByIdSuccessResultContract, 
         IGetClientStringByIdErrorResultContract>
    {
        public GetClientStringById(IBus bus, IGetClientStringByIdRequestContract request) : base(bus, request) {}
    }
    
    public class GetWorkerStringById : 
        MasstransitRequest
        <IGetWorkerStringByIdRequestContract, 
         IGetWorkerStringByIdResultContract,
         IGetWorkerStringByIdSuccessResultContract, 
         IGetWorkerStringByIdErrorResultContract>
    {
        public GetWorkerStringById(IBus bus, IGetWorkerStringByIdRequestContract request) : base(bus, request) {}
    }
    
    public class GetInstanceStringById : 
        MasstransitRequest
        <IGetInstanceStringByIdRequestContract, 
         IGetInstanceStringByIdResultContract,
         IGetInstanceStringByIdSuccessResultContract, 
         IGetInstanceStringByIdErrorResultContract>
    {
        public GetInstanceStringById(IBus bus, IGetInstanceStringByIdRequestContract request) : base(bus, request) {}
    }
}