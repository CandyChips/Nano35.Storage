using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllCancellations
{
    public class LoggedGetAllCancellationsRequest :
        PipeNodeBase<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract>
    {
        private readonly ILogger<LoggedGetAllCancellationsRequest> _logger;
        
        public LoggedGetAllCancellationsRequest(
            ILogger<LoggedGetAllCancellationsRequest> logger,
            IPipeNode<IGetAllCancellationsRequestContract, IGetAllCancellationsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllCancellationsResultContract> Ask(
            IGetAllCancellationsRequestContract input)
        {
            _logger.LogInformation($"GetAllCancellationsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllCancellationsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}