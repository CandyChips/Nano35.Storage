using System;
using System.Collections.Generic;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.HttpContext
{
    public class CreateMoveHttpContext
    {
        public class CreateMoveRequest :
            ICreateMoveRequestContract
        {
            public Guid NewId { get; set; }
            public Guid InstanceId { get; set; }
            public Guid FromUnitId { get; set; }
            public Guid ToUnitId { get; set; }
            public string Number { get; set; }
            public IEnumerable<ICreateMoveRequestContract.ICreateMoveDetailViewModel> Details { get; set; }
        }

        public class CreateMoveHeader
        {
            public Guid NewId { get; set; }
            public Guid InstanceId { get; set; }
        }

        public class CreateMoveBody
        {
            public Guid FromUnitId { get; set; }
            public Guid ToUnitId { get; set; }
            public string Number { get; set; }
            public IEnumerable<ICreateMoveRequestContract.ICreateMoveDetailViewModel> Details { get; set; }
        }
    }
}