using System;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.HttpContext
{
    public class GetAllArticlesCategoriesHttpContext : 
        IGetAllArticlesCategoriesRequestContract
    {
        public Guid InstanceId { get; set; }
        public Guid ParentId { get; set; }
    }
}