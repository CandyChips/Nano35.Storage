using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class LoggedGetAllArticlesModelsRequest :
        PipeNodeBase<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract>
    {
        private readonly ILogger<LoggedGetAllArticlesModelsRequest> _logger;
        
        public LoggedGetAllArticlesModelsRequest(
            ILogger<LoggedGetAllArticlesModelsRequest> logger,
            IPipeNode<IGetAllArticlesModelsRequestContract, IGetAllArticlesModelsResultContract> next) :
            base(next)
        {
            _logger = logger;
        }

        public override async Task<IGetAllArticlesModelsResultContract> Ask(
            IGetAllArticlesModelsRequestContract input)
        {
            _logger.LogInformation($"GetAllArticlesModelsLogger starts on: {DateTime.Now}");
            var result = await DoNext(input);
            _logger.LogInformation($"GetAllArticlesModelsLogger ends on: {DateTime.Now}");
            return result;
        }
    }
}