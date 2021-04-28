using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCategory
{
    public class CreateCategoryConsumer : 
        IConsumer<ICreateCategoryRequestContract>
    {
        private readonly IServiceProvider _services;

        public CreateCategoryConsumer(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<ICreateCategoryRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<ICreateCategoryRequestContract, ICreateCategoryResultContract>(
                _services.GetService(typeof(ILogger<ICreateCategoryRequestContract>)) as ILogger<ICreateCategoryRequestContract>,
                new TransactedPipeNode<ICreateCategoryRequestContract, ICreateCategoryResultContract>(dbContext,
                    new CreateCategoryRequest(dbContext))).Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case ICreateCategorySuccessResultContract:
                    await context.RespondAsync<ICreateCategorySuccessResultContract>(result);
                    break;
                case ICreateCategoryErrorResultContract:
                    await context.RespondAsync<ICreateCategoryErrorResultContract>(result);
                    break;
            }
        }
    }
}