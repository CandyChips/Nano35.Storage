using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllSelleDetails
{
    public class GetAllSelleDetailsConsumer : 
        IConsumer<IGetAllSelleDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllSelleDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllSelleDetailsRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllSelleDetailsRequestContract>) _services.GetService(typeof(ILogger<IGetAllSelleDetailsRequestContract>));

            var message = context.Message;
            var result =
                await new LoggedPipeNode<IGetAllSelleDetailsRequestContract, IGetAllSelleDetailsResultContract>(logger,
                    new GetAllSelleDetailsRequest(dbContext, bus)).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IGetAllSelleDetailsSuccessResultContract:
                    await context.RespondAsync<IGetAllSelleDetailsSuccessResultContract>(result);
                    break;
                case IGetAllSelleDetailsErrorResultContract:
                    await context.RespondAsync<IGetAllSelleDetailsErrorResultContract>(result);
                    break;
            }
        }
    }
}