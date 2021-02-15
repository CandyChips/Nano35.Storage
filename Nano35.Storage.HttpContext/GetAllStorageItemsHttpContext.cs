using System;
using System.Collections.Generic;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.HttpContext
{
    public class GetAllStorageItemsHttpContext
    {
        public class GetAllStorageItemsHeader
        {
        }
        
        public class GetAllStorageItemsQuery 
        {
            public Guid InstanceId { get; set; }
        }
        
        public class GetAllStorageItemsRequest :
            IGetAllStorageItemsRequestContract
        {
            public Guid InstanceId { get; set; }
        }
    }
    
    public class CreateComingHttpContext
    {
        public class ComingDetail :
            ICreateComingRequestContract.ICreateComingDetailViewModel
        {
            public Guid NewId { get; set; }
            public int Count { get; set; }
            public string PlaceOnStorage { get; set; }
            public Guid StorageItemId { get; set; }
            public double Price { get; set; }
        }
        public class CreateComingBody
        {
            public Guid NewId { get; set; }
            public Guid InstanceId { get; set; }
            public Guid UnitId { get; set; }
            public string Number { get; set; }
            public string Comment { get; set; }
            public Guid ClientId { get; set; }
            public IEnumerable<ComingDetail> Details { get; set; }
        }
        public class CreateComingRequest :
            ICreateComingRequestContract
        {
            public Guid NewId { get; set; }
            public Guid InstanceId { get; set; }
            public Guid UnitId { get; set; }
            public string Number { get; set; }
            public string Comment { get; set; }
            public Guid ClientId { get; set; }
            public IEnumerable<ICreateComingRequestContract.ICreateComingDetailViewModel> Details { get; set; }
        }
    }
}