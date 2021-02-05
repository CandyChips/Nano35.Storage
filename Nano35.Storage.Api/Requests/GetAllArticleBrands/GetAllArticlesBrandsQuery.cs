using System;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Api.Requests.Behaviours;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class GetAllArticleBrandsQuery : 
        IGetAllArticlesBrandsRequestContract, 
        IQueryRequest<IGetAllArticlesBrandsResultContract>
    {
        public Guid InstanceId { get; set; }
        public Guid CategoryId { get; set; }
    }
}