using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateSalle
{
    public class CreateSelleConsumer : 
        IConsumer<ICreateSelleRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateSelleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateSelleRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<ICreateSelleRequestContract>) _services.GetService(typeof(ILogger<ICreateSelleRequestContract>));
            var message = context.Message;
            var result = await new LoggedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(logger,
                    new ValidatedCreateSelleRequest(
                        new TransactedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(dbContext,
                            new CreateSelleRequest(dbContext)))).Ask(message, context.CancellationToken);
            switch (result)
            {
                case ICreateSelleSuccessResultContract:
                    await context.RespondAsync<ICreateSelleSuccessResultContract>(result);
                    break;
                case ICreateSelleErrorResultContract:
                    await context.RespondAsync<ICreateSelleErrorResultContract>(result);
                    break;
            }
        }
    }
}