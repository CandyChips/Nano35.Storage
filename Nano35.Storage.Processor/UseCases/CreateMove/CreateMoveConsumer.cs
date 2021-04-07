using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateMove
{
    public class CreateMoveConsumer : 
        IConsumer<ICreateMoveRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateMoveConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateMoveRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<ICreateMoveRequestContract>) _services
                .GetService(typeof(ILogger<ICreateMoveRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(logger,
                        new TransactedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(dbContext,
                            new CreateMoveRequest(dbContext))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case ICreateMoveSuccessResultContract:
                    await context.RespondAsync<ICreateMoveSuccessResultContract>(result);
                    break;
                case ICreateMoveErrorResultContract:
                    await context.RespondAsync<ICreateMoveErrorResultContract>(result);
                    break;
            }
        }
    }
}