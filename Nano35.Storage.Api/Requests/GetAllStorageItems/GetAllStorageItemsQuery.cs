using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.GetAllStorageItems
{
    public class GetAllStorageItemsQuery : 
        IGetAllStorageItemsRequestContract, 
        IQueryRequest<IGetAllStorageItemsResultContract>
    {
        public Guid InstanceId { get; set; }
        
    }
}