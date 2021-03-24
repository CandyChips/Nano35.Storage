using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateComing
{
    public class LoggedCreateComingRequest :
        PipeNodeBase<ICreateComingRequestContract, ICreateComingResultContract>
    {
        private readonly ILogger<LoggedCreateComingRequest> _logger;

        public LoggedCreateComingRequest(
            ILogger<LoggedCreateComingRequest> logger,
            IPipeNode<ICreateComingRequestContract,  ICreateComingResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateComingResultContract> Ask(
            ICreateComingRequestContract input)
        {
            _logger.LogInformation($"CreateComingLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"CreateComingLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}