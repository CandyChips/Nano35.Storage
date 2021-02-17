using System;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.HttpContext
{
    public class GetStorageItemByIdHttpContext : 
        IGetStorageItemByIdRequestContract
    {
        public Guid Id { get; set; }
    }
    
    public class GetComingDetailsByIdHttpContext :
        IGetComingDetailsByIdRequestContract
    {
        public Guid Id { get; set; }
    }
}