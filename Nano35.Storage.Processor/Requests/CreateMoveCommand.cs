using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Requests
{
    public class CreateMoveCommand :
        ICreateMoveRequestContract, 
        ICommandRequest<ICreateMoveResultContract>
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid FromUnitId { get; set; }
        public Guid ToUnitId { get; set; }
        public string Number { get; set; }
        public IEnumerable<ICreateMoveRequestContract.ICreateMoveDetailViewModel> Details { get; set; }

        private class CreateMoveSuccessResultContract : 
            ICreateMoveSuccessResultContract
        {
            
        }

        private class CreateMoveErrorResultContract :
            ICreateMoveErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateMoveHandler : 
            IRequestHandler<CreateMoveCommand, ICreateMoveResultContract>
        {
            private readonly ApplicationContext _context;
            
            public CreateMoveHandler(ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<ICreateMoveResultContract> Handle(
                CreateMoveCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    // ToDo !!!
                    
                    return new CreateMoveSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateMoveErrorResultContract();
                }
            }
        }
    }
}