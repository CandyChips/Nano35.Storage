using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateStorageItem
{
    public class CreateStorageItemUseCase :
        UseCaseEndPointNodeBase<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>
    {
        private readonly IBus _bus;
        public CreateStorageItemUseCase(IBus bus) { _bus = bus; }
        
        public override async Task<UseCaseResponse<ICreateStorageItemResultContract>> Ask(ICreateStorageItemRequestContract input)
        {
            
            if (input.NewId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");
            if (input.InstanceId == Guid.Empty)
                return Pass("Обновите страницу и попробуйте еще раз");
            if (input.ArticleId == Guid.Empty)
                return Pass("Не выбрано название предмета");
            
            var checkExistArticleRequestContract = 
                new MasstransitUseCaseRequest<ICheckExistArticleRequestContract, ICheckExistArticleResultContract>(_bus, new CheckExistArticleRequestContract() {ArticleId = input.ArticleId}).GetResponse().Result;
            if (checkExistArticleRequestContract.IsSuccess())
                if (!checkExistArticleRequestContract.Success.Exist)
                    return Pass("Не верное название устройства");
            
            if (input.ConditionId == Guid.Empty)
                return Pass("Не выбрано состояние предмета");

            return await new MasstransitUseCaseRequest<ICreateStorageItemRequestContract, ICreateStorageItemResultContract>(
                    _bus, input)
                .GetResponse();
        }
    }
}