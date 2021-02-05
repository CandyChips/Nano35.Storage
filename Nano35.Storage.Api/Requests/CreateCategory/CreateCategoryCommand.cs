using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.CreateCategory
{
    public class CreateCategoryCommand :
        ICreateCategoryRequestContract, 
        ICommandRequest<ICreateCategoryResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public Guid ParentCategoryId { get; set; }

    }
}