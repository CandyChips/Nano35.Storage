using System;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.HttpContext
{
    public class GetAllArticlesHttpContext : 
        IGetAllArticlesRequestContract
    {
        public Guid InstanceId { get; set; }
    }
}