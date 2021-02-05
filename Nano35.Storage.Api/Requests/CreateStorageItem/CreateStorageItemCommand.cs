using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemCommand :
        ICreateStorageItemRequestContract, 
        ICommandRequest<ICreateStorageItemResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Comment { get; set; }
        public string HiddenComment { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public Guid ArticleId { get; set; }
        public Guid ConditionId { get; set; }
    }
}