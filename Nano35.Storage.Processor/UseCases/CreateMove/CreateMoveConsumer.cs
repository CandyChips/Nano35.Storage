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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(
                _services.GetService(typeof(ILogger<ICreateMoveRequestContract>)) as ILogger<ICreateMoveRequestContract>,
                new TransactedPipeNode<ICreateMoveRequestContract, ICreateMoveResultContract>(dbContext,
                    new CreateMoveRequest(dbContext))).Ask(context.Message, context.CancellationToken);
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