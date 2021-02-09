using System;
using System.Text.Json.Serialization;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.HttpContext
{
    public class CreateStorageItemHttpContext : 
        ICreateStorageItemRequestContract
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Comment { get; set; }
        public string HiddenComment { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        [JsonIgnore]
        public string[] FileSource { get; set; }
        public Guid ArticleId { get; set; }
        public Guid ConditionId { get; set; }
    }
}