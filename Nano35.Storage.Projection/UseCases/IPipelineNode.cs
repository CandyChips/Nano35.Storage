﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts;

namespace Nano35.Storage.Projection.UseCases
{
    public interface IPipeNode<in TIn, TOut>
    {
        Task<TOut> Ask(TIn input);
    }
    
    public interface IPipelineNode<in TIn, TOut>
    {
        Task<TOut> Ask(TIn input);
    }
    
    public abstract class PipeInConvert <TFrom, TTo, In, TOut> : 
        IPipeNode<TFrom, TTo>
    {
        private readonly IPipeNode<In, TOut> _next;
        protected PipeInConvert(IPipeNode<In, TOut> next) { _next = next; }
        protected Task<TOut> DoNext(In input) { return _next.Ask(input); }
        public abstract Task<TTo> Ask(TFrom input);
    }

    public abstract class PipeNodeBase<TIn, TOut> : 
        IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        private readonly IPipeNode<TIn, TOut> _next;
        protected PipeNodeBase(IPipeNode<TIn, TOut> next) { _next = next; }
        protected Task<TOut> DoNext(TIn input) { return _next.Ask(input); }
        public abstract Task<TOut> Ask(TIn input);
    }

    public abstract class EndPointNodeBase<TIn, TOut> : 
        IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResponse
    {
        public abstract Task<TOut> Ask(TIn input);
    }

    /// <summary>
    /// Contract request reduction
    /// TMessage -> TResponse => ( TSuccess / TError )
    /// </summary>
    /// <typeparam name="TMessage">Is class and IRequest</typeparam>
    /// <typeparam name="TResponse">Is class and IResponse</typeparam>
    /// <typeparam name="TSuccess">Is class ISuccess and IResponse</typeparam>
    /// <typeparam name="TError">Is class IError and IResponse</typeparam>
    public abstract class MasstransitRequest<TMessage, TResponse, TSuccess, TError> 
        where TMessage : class, IRequest
        where TResponse : class, IResponse
        where TSuccess : class, ISuccess, TResponse
        where TError : class, IError, TResponse
    {
        private readonly IRequestClient<TMessage> _requestClient;

        protected MasstransitRequest(IBus bus)
        {
            _requestClient = bus.CreateRequestClient<TMessage>(TimeSpan.FromSeconds(10));
        }

        public async Task<TResponse> GetResponse(TMessage request)
        {
            var responseGetClientString = await _requestClient.GetResponse<TSuccess, TError>(request);
            if (responseGetClientString.Is(out Response<TSuccess> successResponse))
                return successResponse.Message;
            if (responseGetClientString.Is(out Response<TError> errorResponse))
                return errorResponse.Message;
            throw new Exception();
        }
    }

    public abstract class EndPointRequestNodeBase<TMessage, TResponse, TSuccess, TError>  : 
        MasstransitRequest<TMessage, TResponse, TSuccess, TError>,
        IPipeNode<TMessage, TResponse>
        where TMessage : class, IRequest
        where TResponse : class, IResponse
        where TSuccess : class, ISuccess, TResponse
        where TError : class, IError, TResponse
    {
        protected EndPointRequestNodeBase(IBus bus) : base(bus) {}
        public async Task<TResponse> Ask(TMessage input) => await GetResponse(input);
    }
}