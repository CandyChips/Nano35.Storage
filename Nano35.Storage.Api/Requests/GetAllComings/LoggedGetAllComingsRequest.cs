using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllComings
{
    public class LoggedGetAllComingsRequest :
        PipeNodeBase<IGetAllComingsRequestContract, IGetAllComingsResultContract>
    {
        private readonly ILogger<LoggedGetAllComingsRequest> _logger;

        public LoggedGetAllComingsRequest(
            ILogger<LoggedGetAllComingsRequest> logger,
            IPipeNode<IGetAllComingsRequestContract, IGetAllComingsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllComingsResultContract> Ask(
            IGetAllComingsRequestContract input)
        {
            _logger.LogInformation($"GetAllComingsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllComingsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}