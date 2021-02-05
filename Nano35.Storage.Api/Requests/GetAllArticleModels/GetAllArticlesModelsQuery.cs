using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.GetAllArticleModels
{
    public class GetAllArticlesModelsQuery : 
        IGetAllArticlesModelsRequestContract, 
        IQueryRequest<IGetAllArticlesModelsResultContract>
    {
        public Guid InstanceId { get; set; }
        public Guid CategoryId { get; set; }
    }
}