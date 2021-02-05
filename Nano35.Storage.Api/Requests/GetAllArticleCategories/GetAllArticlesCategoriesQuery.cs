using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.GetAllArticleCategories
{
    public class GetAllArticlesCategoriesQuery : 
        IGetAllArticlesCategoriesRequestContract,
        IQueryRequest<IGetAllArticlesCategoriesResultContract>
    {
        public Guid InstanceId { get; set; }
        public Guid ParentId { get; set; }
    }
}