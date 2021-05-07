using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;

namespace Nano35.Storage.Api.Requests.CreateArticle
{
    public class CreateArticleUseCase : UseCaseEndPointNodeBase<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly IBus _bus;
        public CreateArticleUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<ICreateArticleResultContract>> Ask(ICreateArticleRequestContract input)
        { 
            if (input.NewId == Guid.Empty) return Pass("Обновите страницу и попробуйте еще раз");
            if (input.InstanceId == Guid.Empty) return Pass("Обновите страницу и попробуйте еще раз");
            if (input.CategoryId == Guid.Empty) return Pass("Не выбрана категория устройства");
            if (input.Specs.Any()) return Pass("Нет спецификаций устройства");
            if (string.IsNullOrEmpty(input.Brand)) return Pass("Нет бренда устройства-");
            if (string.IsNullOrEmpty(input.Model)) return Pass("Нет модели устройства");
            
            return await new MasstransitUseCaseRequest<ICreateArticleRequestContract, ICreateArticleResultContract>(_bus,
                    input)
                .GetResponse();
        }
    }
}