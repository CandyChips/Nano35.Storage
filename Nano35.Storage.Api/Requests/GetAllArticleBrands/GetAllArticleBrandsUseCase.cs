using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.GetAllArticleBrands
{
    public class GetAllArticleBrandsUseCase :
        EndPointNodeBase<IGetAllArticlesBrandsRequestContract, IGetAllArticlesBrandsResultContract>
    {
        private readonly IBus _bus;
        
        public GetAllArticleBrandsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllArticlesBrandsResultContract> Ask(
            IGetAllArticlesBrandsRequestContract input) => 
            (await (new GetAllArticleBrandsRequest(_bus)).GetResponse(input));
    }
}