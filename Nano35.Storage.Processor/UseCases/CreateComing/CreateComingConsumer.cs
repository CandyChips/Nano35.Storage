using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateComing
{
    public class CreateComingConsumer : 
        IConsumer<ICreateComingRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateComingConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateComingRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<ICreateComingRequestContract>) _services
                .GetService(typeof(ILogger<ICreateComingRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(logger,
                        new TransactedPipeNode<ICreateComingRequestContract, ICreateComingResultContract>(dbContext,
                            new CreateComingRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateComingSuccessResultContract:
                    await context.RespondAsync<ICreateComingSuccessResultContract>(result);
                    break;
                case ICreateComingErrorResultContract:
                    await context.RespondAsync<ICreateComingErrorResultContract>(result);
                    break;
            }
        }
    }
}