using System;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.HttpContext
{
    public class GetAllArticlesHttpContext : 
        IGetAllArticlesRequestContract
    {
        public Guid InstanceId { get; set; }
    }
}