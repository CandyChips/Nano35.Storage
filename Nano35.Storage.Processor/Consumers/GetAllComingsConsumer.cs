using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Requests.GetAllArticle;
using Nano35.Storage.Processor.Requests.GetAllComings;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Consumers
{
    public class GetAllComingsConsumer : 
        IConsumer<IGetAllComingsRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllComingsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllComingsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllComingsLogger>) _services.GetService(typeof(ILogger<GetAllComingsLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new GetAllComingsLogger(logger,
                    new GetAllComingsValidator(
                        new GetAllComingsRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create article request
            switch (result)
            {
                case IGetAllComingsSuccessResultContract:
                    await context.RespondAsync<IGetAllComingsSuccessResultContract>(result);
                    break;
                case IGetAllComingsErrorResultContract:
                    await context.RespondAsync<IGetAllComingsErrorResultContract>(result);
                    break;
            }
        }
    }
}