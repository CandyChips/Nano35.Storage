using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.GetAllArticleBrands
{
    public class GetAllArticlesBrandsConsumer : 
        IConsumer<IGetAllArticlesBrandsRequestContract>
    {
        private readonly IServiceProvider _services;
        public GetAllArticlesBrandsConsumer(IServiceProvider services) { _services = services; }
        public async Task Consume(
            ConsumeContext<IGetAllArticlesBrandsRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract>(
                _services.GetService(typeof(ILogger<IGetAllArticlesBrandsRequestContract>)) as ILogger<IGetAllArticlesBrandsRequestContract>,
                new GetAllArticlesBrandsRequest(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetAllArticlesBrandsSuccessResultContract:
                    await context.RespondAsync<IGetAllArticlesBrandsSuccessResultContract>(result);
                    break;
                case IGetAllArticlesBrandsErrorResultContract:
                    await context.RespondAsync<IGetAllArticlesBrandsErrorResultContract>(result);
                    break;
            }
        }
    }
}