using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllWarehouseNames
{
    public class GetAllWarehouseNamesConsumer : 
        IConsumer<IGetAllWarehouseNamesRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetAllWarehouseNamesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWarehouseNamesRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWarehouseNamesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllWarehouseNamesRequest>));

            var message = context.Message;

            var result = await new LoggedGetAllWarehouseNamesRequest(logger,
                        new ValidatedGetAllWarehouseNamesRequest(
                            new GetAllWarehouseNamesRequest(dbContext, bus))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllWarehouseNamesSuccessResultContract:
                    await context.RespondAsync<IGetAllWarehouseNamesSuccessResultContract>(result);
                    break;
                case IGetAllWarehouseNamesErrorResultContract:
                    await context.RespondAsync<IGetAllWarehouseNamesErrorResultContract>(result);
                    break;
            }
        }
    }
}