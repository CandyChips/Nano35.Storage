using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllComings
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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllComingsRequestContract>) _services.GetService(typeof(ILogger<IGetAllComingsRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract>(logger,
                new GetAllComingsRequest(dbContext, bus)).Ask(message, context.CancellationToken);
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