using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.GetAllArticles
{
    public class GetAllArticlesQuery : 
        IGetAllArticlesRequestContract, 
        IQueryRequest<IGetAllArticlesResultContract>
    {
        public Guid InstanceId { get; set; }
        
    }
}