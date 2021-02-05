using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.GetStorageItemById
{
    public class GetStorageItemByIdQuery : 
        IGetStorageItemByIdRequestContract, 
        IQueryRequest<IGetStorageItemByIdResultContract>
    {
        public Guid Id { get; set; }
        

    }
}