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
    public class CreateComingCommand :
        ICreateComingRequestContract, 
        ICommandRequest<ICreateComingResultContract>
    {
        public Guid NewId { get; set; }
        public Guid IntsanceId { get; set; }
        public Guid UnitId { get; set; }
        public string Number { get; set; }
        public string Comment { get; set; }
        public Guid ClientId { get; set; }
        public IEnumerable<ICreateComingRequestContract.ICreateComingDetailViewModel> Details { get; set; }

        private class CreateComingSuccessResultContract : 
            ICreateComingSuccessResultContract
        {
            
        }

        private class CreateComingErrorResultContract :
            ICreateComingErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateComingHandler : 
            IRequestHandler<CreateComingCommand, ICreateComingResultContract>
        {
            private readonly ApplicationContext _context;
            
            public CreateComingHandler(ApplicationContext context)
            {
                _context = context;
            }
        
            public async Task<ICreateComingResultContract> Handle(
                CreateComingCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    // ToDo !!!
                    
                    return new CreateComingSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateComingErrorResultContract();
                }
            }
        }
    }
}