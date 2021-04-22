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
            var result = await new LoggedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                _services.GetService(typeof(ILogger<ICreateSelleRequestContract>)) as ILogger<ICreateSelleRequestContract>,
                new TransactedPipeNode<ICreateSelleRequestContract, ICreateSelleResultContract>(
                    dbContext,
                    new CreateSelleRequest(
                        dbContext,
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(context.Message, context.CancellationToken);
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