using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Requests.Behaviours
{
    public class LoggerCommandDecorator<TIn, TOut> :
        IPipelineBehavior<TIn, TOut>
        where TIn : ICommandRequest<TOut>
    {
        private readonly ILogger<LoggerCommandDecorator<TIn, TOut>> _logger;

        public LoggerCommandDecorator(
            ILogger<LoggerCommandDecorator<TIn, TOut>> logger)
        {
            _logger = logger;
        }

        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            _logger.LogInformation($"Before Command - {DateTime.Now}");
            var response = await next();
            _logger.LogInformation($"After Command - {DateTime.Now}");
            return response;
        }
    }
}