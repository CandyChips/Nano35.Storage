using System;
using System.Collections.Generic;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.HttpContext
{
    public class SpecHttpContext :
        ISpecVievModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    
    public class CreateArticleHttpContext : 
        ICreateArticleRequestContract
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public Guid CategoryId { get; set; }
        public string Info { get; set; }
        public IEnumerable<Tuple<string,string>> Specs { get; set; }
    }
}
