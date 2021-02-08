using System;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.HttpContext
{
    public class CreateCategoryHttpContext : 
        ICreateCategoryRequestContract
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public Guid ParentCategoryId { get; set; }
    }
}