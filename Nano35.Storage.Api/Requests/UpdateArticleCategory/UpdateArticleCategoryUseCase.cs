using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Contracts.Storage.Models;

namespace Nano35.Storage.Api.Requests.UpdateArticleCategory
{
    public class UpdateArticleCategoryUseCase :
        EndPointNodeBase<IUpdateArticleCategoryRequestContract, IUpdateArticleCategoryResultContract>
    {
        private readonly IBus _bus;
        public UpdateArticleCategoryUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IUpdateArticleCategoryResultContract> Ask(
            IUpdateArticleCategoryRequestContract input) => 
            (await (new UpdateArticleCategoryRequest(_bus)).GetResponse(input));
    }
}