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
    public class CreateCancelationHttpContext : 
        ICreateCancelationRequestContract
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid UnitId { get; set; }
        public string Number { get; set; }
        public string Comment { get; set; }
        public IEnumerable<ICreateCancelationRequestContract.ICreateCancelationDetailViewModel> Details { get; set; }
    }
    
    public class CreateComingHttpContext :
        ICreateComingRequestContract
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid UnitId { get; set; }
        public string Number { get; set; }
        public string Comment { get; set; }
        public Guid ClientId { get; set; }
        public IEnumerable<ICreateComingRequestContract.ICreateComingDetailViewModel> Details { get; set; }
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