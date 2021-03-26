using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleInfo
{
    public class UpdateArticleInfoUseCase :
        EndPointNodeBase<IUpdateArticleInfoRequestContract, IUpdateArticleInfoResultContract>
    {
        private readonly IBus _bus;
        
        public UpdateArticleInfoUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateArticleInfoResultContract> Ask(
            IUpdateArticleInfoRequestContract input) => 
            (await (new UpdateArticleInfoRequest(_bus)).GetResponse(input));
    }
}