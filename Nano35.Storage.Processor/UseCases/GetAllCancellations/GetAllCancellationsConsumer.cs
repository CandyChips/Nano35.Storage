using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellations
{
    public class GetAllCancellationsConsumer : 
        IConsumer<IGetAllCancellationsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllCancellationsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllCancellationsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllCancellationsRequestContract>) _services
                .GetService(typeof(ILogger<IGetAllCancellationsRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>(logger,
                    new ValidatedGetAllCancellationsRequest(
                        new GetAllCancellationsRequest(dbContext, bus))).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllCancellationsSuccessResultContract:
                    await context.RespondAsync<IGetAllCancellationsSuccessResultContract>(result);
                    break;
                case IGetAllCancellationsErrorResultContract:
                    await context.RespondAsync<IGetAllCancellationsErrorResultContract>(result);
                    break;
            }
        }
    }
}