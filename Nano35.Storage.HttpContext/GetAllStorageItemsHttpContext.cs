using System;
using System.Collections.Generic;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.HttpContext
{
    public class GetAllStorageItemsHttpContext : 
        IGetAllStorageItemsRequestContract
    {
        public Guid InstanceId { get; set; }
    }
    
    public class CreateCancelationRequestContract :
        ICreateCancelationRequestContract
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid UnitId { get; set; }
        public string Number { get; set; }
        public string Comment { get; set; }
        public IEnumerable<ICreateCancelationRequestContract.ICreateCancelationDetailViewModel> Details {get; set;}
    }
    public class CreateCancelationHttpContext
    {
        public class CancellationDetail :
            ICreateCancelationRequestContract.ICreateCancelationDetailViewModel
        {
            public int Count { get; set; }
            public string PlaceOnStorage { get; set; }
            public Guid StringItemId { get; set; }
        }

        public class Header
        {
            public Guid NewId { get; set; }
        }
        
        public class Body
        {
            public Guid NewId { get; set; }
            public Guid IntsanceId { get; set; }
            public Guid UnitId { get; set; }
            public string Number { get; set; }
            public string Comment { get; set; }
            public IEnumerable<CancellationDetail> Details { get; set; }
        }
    }
        
    public class CreateComingRequestContract :
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
        public class Header
        {
            public Guid NewId { get; set; }
        }
        public class Body
        {
            public Guid NewId { get; set; }
            public Guid InstanceId { get; set; }
            public Guid UnitId { get; set; }
            public string Number { get; set; }
            public string Comment { get; set; }
            public Guid ClientId { get; set; }
            public IEnumerable<ComingDetail> Details { get; set; }
        }
    }
    
    public class CreateMoveHttpContext :
        ICreateMoveRequestContract
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid FromUnitId { get; set; }
        public Guid ToUnitId { get; set; }
        public string Number { get; set; }
        public IEnumerable<ICreateMoveRequestContract.ICreateMoveDetailViewModel> Details { get; set; }
    }
    
    public class CreateSalleHttpContext :
        ICreateSalleRequestContract
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid UnitId { get; set; }
        public string Number { get; set; }
        public Guid ClientId { get; set; }
        public IEnumerable<ICreateSalleRequestContract.ICreateSalleDetailViewModel> Details { get; set; }
    }
}