using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateCancellation
{
    public class CreateCancellationConsumer : 
        IConsumer<ICreateCancellationRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateCancellationConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateCancellationRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<ICreateCancellationRequestContract>) _services.GetService(typeof(ILogger<ICreateCancellationRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(logger,
                    new ValidatedCreateCancellationRequest(
                        new TransactedPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract>(dbContext,
                            new CreateCancellationRequest(dbContext)))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case ICreateCancellationSuccessResultContract:
                    await context.RespondAsync<ICreateCancellationSuccessResultContract>(result);
                    break;
                case ICreateCancellationErrorResultContract:
                    await context.RespondAsync<ICreateCancellationErrorResultContract>(result);
                    break;
            }
        }
    }
}