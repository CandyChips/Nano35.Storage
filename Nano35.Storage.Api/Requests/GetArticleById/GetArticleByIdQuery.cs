using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.GetArticleById
{
    public class GetArticleByIdQuery : 
        IGetArticleByIdRequestContract, 
        IQueryRequest<IGetArticleByIdResultContract>
    {
        public Guid Id { get; set; }
        

    }
}