using MediatR;

namespace Nano35.Storage.Api.Requests.Behaviours
{
    public interface IQueryRequest<TOut> :
        IRequest<TOut>
    {
        
    }
    
    public interface ICommandRequest<TOut> :
        IRequest<TOut>
    {
        
    }
}