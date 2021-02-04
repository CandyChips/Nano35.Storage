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
    public class CreateCategoryCommand :
        ICreateCategoryRequestContract, 
        ICommandRequest<ICreateCategoryResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public Guid ParentCategoryId { get; set; }

        private class CreateCategorySuccessResultContract : 
            ICreateCategorySuccessResultContract
        {
            
        }

        private class CreateCategoryErrorResultContract :
            ICreateCategoryErrorResultContract
        {
            public string Message { get; set; }
        }

        public class CreateCategoryHandler : 
            IRequestHandler<CreateCategoryCommand, ICreateCategoryResultContract>
        {
            private readonly ApplicationContext _context;
            private readonly IBus _bus;
            
            public CreateCategoryHandler(
                ApplicationContext context, 
                IBus bus)
            {
                _context = context;
                _bus = bus;
            }
        
            public async Task<ICreateCategoryResultContract> Handle(
                CreateCategoryCommand message, 
                CancellationToken cancellationToken)
            {
                try
                {
                    var category = new Category(){
                        Id = message.NewId,
                        InstanceId = message.InstanceId,
                        ParentCategoryId = message.ParentCategoryId,
                        Name = message.Name,
                        IsDeleted = false
                    };
                    await _context.AddAsync(category, cancellationToken);
                    
                    return new CreateCategorySuccessResultContract();
                }
                catch (Exception e)
                {
                    return new CreateCategoryErrorResultContract();
                }
            }
        }
    }
}