using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.CreateCategory;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class CreateCategoryConsumer : 
        IConsumer<ICreateCategoryRequestContract>
    {
        private readonly IServiceProvider _services;

        public CreateCategoryConsumer(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateCategoryRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateCategoryLogger>) _services.GetService(typeof(ILogger<CreateCategoryLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new CreateCategoryLogger(logger,
                    new CreateCategoryValidator(
                        new CreateCategoryTransaction(dbContext,
                            new CreateCategoryRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
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