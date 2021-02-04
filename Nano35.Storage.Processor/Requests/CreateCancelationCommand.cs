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
    public class CreateCancelationCommand :
        ICreateCancelationRequestContract, 
        ICommandRequest<ICreateCancelationResultContract>
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid UnitId { get; set; }
        public string Number { get; set; }
        public string Comment { get; set; }
        public IEnumerable<ICreateCancelationRequestContract.ICreateCancelationDetailViewModel> Details { get; set; }

        private class CreateCancelationSuccessResultContract : 
            ICreateCancelationSuccessResultContract
        {
            
        }

        private class CreateCancelationErrorResultContract :
            ICreateCancelationErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateCancelationHandler : 
            IRequestHandler<CreateCancelationCommand, ICreateCancelationResultContract>
        {
            private readonly ApplicationContext _context;
            
            public CreateCancelationHandler(ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<ICreateCancelationResultContract> Handle(
                CreateCancelationCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    // ToDo !!!
                    
                    return new CreateCancelationSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateCancelationErrorResultContract();
                }
            }
        }
    }
}