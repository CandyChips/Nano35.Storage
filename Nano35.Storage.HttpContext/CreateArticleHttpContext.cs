using System;
using System.Collections.Generic;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.HttpContext
{
    public class CreateArticleHttpContext : 
        ICreateArticleRequestContract
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public Guid CategoryId { get; set; }
        public string Info { get; set; }
        public IEnumerable<ISpecVievModel> Specs { get; set; }
    }
}
