using System;
using System.Collections.Generic;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.HttpContext
{
    public class CreateCancellationHttpContext
    {
        public class CreateCancellationHeader
        {
            public Guid NewId { get; set; }
            public Guid InstanceId { get; set; }
        }

        public class CreateCancellationBody
        {
            public Guid UnitId { get; set; }
            public string Number { get; set; }
            public string Comment { get; set; }
            public IEnumerable<CancellationDetail> Details { get; set; }

            public class CancellationDetail :
                ICreateCancellationRequestContract.ICreateCancellationDetailViewModel
            {
                public int Count { get; set; }
                public string PlaceOnStorage { get; set; }
                public Guid StorageItemId { get; set; }
            }
        }
    
        public class CreateCancellationRequestContract :
            ICreateCancellationRequestContract
        {
            public Guid NewId { get; set; }
            public Guid InstanceId { get; set; }
            public Guid UnitId { get; set; }
            public string Number { get; set; }
            public string Comment { get; set; }
            public IEnumerable<ICreateCancellationRequestContract.ICreateCancellationDetailViewModel> Details {get; set;}
        }
    }
}