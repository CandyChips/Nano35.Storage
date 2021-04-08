using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllCancellationDetails
{
    public class GetAllCancellationDetailsConsumer : 
        IConsumer<IGetAllCancellationDetailsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllCancellationDetailsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllCancellationDetailsRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllCancellationDetailsRequestContract>) _services.GetService(typeof(ILogger<IGetAllCancellationDetailsRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IGetAllCancellationDetailsRequestContract, IGetAllCancellationDetailsResultContract>(logger,
                new GetAllCancellationDetailsRequest(dbContext)).Ask(message, context.CancellationToken);
            switch (result)
            {
                case IGetAllCancellationDetailsSuccessResultContract:
                    await context.RespondAsync<IGetAllCancellationDetailsSuccessResultContract>(result);
                    break;
                case IGetAllCancellationDetailsErrorResultContract:
                    await context.RespondAsync<IGetAllCancellationDetailsErrorResultContract>(result);
                    break;
            }
        }
    }
}