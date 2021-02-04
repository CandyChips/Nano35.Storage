using System;
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
    public class CreateArticleCommand :
        ICreateArticleRequestContract, 
        ICommandRequest<ICreateArticleResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public Guid CategoryId { get; set; }

        private class CreateArticleSuccessResultContract : 
            ICreateArticleSuccessResultContract
        {
            
        }

        private class CreateArticleErrorResultContract :
            ICreateArticleErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateArticleHandler : 
            IRequestHandler<CreateArticleCommand, ICreateArticleResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateArticleHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<ICreateArticleResultContract> Handle(
                CreateArticleCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var client = new Article(){
                        Id = message.NewId,
                        InstanceId = message.InstanceId,
                        IsDeleted = false,
                        Model = message.Model,
                        Brand = message.Brand,
                        CategoryId = message.CategoryId,
                    };
                    
                    await _context.AddAsync(client, cancellationToken);
                    
                    return new CreateArticleSuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateArticleErrorResultContract();
                }
            }
        }
    }
}