using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateCancellation
{
    public class LoggedCreateCancellationRequest :
        PipeNodeBase<ICreateCancellationRequestContract, ICreateCancellationResultContract>
    {
        private readonly ILogger<LoggedCreateCancellationRequest> _logger;

        public LoggedCreateCancellationRequest(
            ILogger<LoggedCreateCancellationRequest> logger,
            IPipeNode<ICreateCancellationRequestContract, ICreateCancellationResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<ICreateCancellationResultContract> Ask(
            ICreateCancellationRequestContract input)
        {
            _logger.LogInformation($"CreateCancellationLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"CreateCancellationLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}