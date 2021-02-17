using System;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.HttpContext
{
    public class GetArticleByIdHttpContext : 
        IGetArticleByIdRequestContract
    {
        public Guid Id { get; set; }
    }
}