using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllArticle;
using Nano35.Storage.Processor.Requests.GetComingDetailsById;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetComingDetailsByIdConsumer : 
        IConsumer<IGetComingDetailsByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetComingDetailsByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetComingDetailsByIdRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetComingDetailsByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetComingDetailsByIdRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetComingDetailsByIdRequest(logger,
                    new ValidatedGetComingDetailsByIdRequest(
                        new GetComingDetailsByIdRequest(dbContext, bus))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetComingDetailsByIdSuccessResultContract:
                    await context.RespondAsync<IGetComingDetailsByIdSuccessResultContract>(result);
                    break;
                case IGetComingDetailsByIdErrorResultContract:
                    await context.RespondAsync<IGetComingDetailsByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}