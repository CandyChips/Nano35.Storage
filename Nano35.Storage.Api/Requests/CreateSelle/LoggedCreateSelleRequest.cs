using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateSelle
{
    public class LoggedCreateSelleRequest :
        PipeNodeBase<ICreateSelleRequestContract, ICreateSelleResultContract>
    {
        private readonly ILogger<LoggedCreateSelleRequest> _logger;

        public LoggedCreateSelleRequest(
            ILogger<LoggedCreateSelleRequest> logger,
            IPipeNode<
                ICreateSelleRequestContract, 
                ICreateSelleResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateSelleResultContract> Ask(
            ICreateSelleRequestContract input)
        {
            _logger.LogInformation($"CreateSelleLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"CreateSelleLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}