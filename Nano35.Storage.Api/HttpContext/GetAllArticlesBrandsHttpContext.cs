using System;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.HttpContext
{
    public class GetAllArticlesBrandsHttpContext : 
        IGetAllArticlesBrandsRequestContract
    {
        public Guid InstanceId { get; set; }
        public Guid CategoryId { get; set; }
    }
}