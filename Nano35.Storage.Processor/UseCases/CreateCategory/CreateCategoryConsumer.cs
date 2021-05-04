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
            var result =
                await new LoggedUseCasePipeNode<ICreateCategoryRequestContract, ICreateCategoryResultContract>(
                        _services.GetService(typeof(ILogger<ICreateCategoryRequestContract>)) as
                            ILogger<ICreateCategoryRequestContract>,
                        new TransactedUseCasePipeNode<ICreateCategoryRequestContract,
                            ICreateCategoryResultContract>(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext,
                            new CreateCategoryRequest(
                                _services.GetService(typeof(ApplicationContext)) as ApplicationContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}