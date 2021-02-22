using System;
using System.Collections.Generic;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.HttpContext
{
    public class GetAllComingsHttpContext
    {

        public class GetAllComingsRequest :
            IGetAllComingsRequestContract
        {
            public Guid InstanceId { get; set; }
            public Guid UnitId { get; set; }
            public Guid StorageItemId { get; set; }
        }

        public class GetAllComingsHeader
        {
            public Guid InstanceId { get; set; }
        }

        public class GetAllComingsQuery
        {
            public Guid UnitId { get; set; }
            public Guid StorageItemId { get; set; }
        }
    }
    public class GetAllSellsHttpContext
    {

        public class GetAllSellsRequest 
        {
            public Guid InstanceId { get; set; }
            public Guid UnitId { get; set; }
            public Guid StorageItemId { get; set; }
        }

        public class GetAllComingsHeader
        {
            public Guid InstanceId { get; set; }
        }

        public class GetAllComingsQuery
        {
            public Guid UnitId { get; set; }
            public Guid StorageItemId { get; set; }
        }
    }
}