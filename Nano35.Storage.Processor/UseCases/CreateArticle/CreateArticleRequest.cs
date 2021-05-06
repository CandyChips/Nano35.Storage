using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Storage.Artifacts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.UseCases.CreateArticle
{
    public class CreateArticleRequest :
        UseCaseEndPointNodeBase<ICreateArticleRequestContract, ICreateArticleResultContract>
    {
        private readonly ApplicationContext _context;

        public CreateArticleRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        public override async Task<UseCaseResponse<ICreateArticleResultContract>> Ask(
            ICreateArticleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (input.NewId == Guid.Empty) return Pass("Обновите страницу и попробуйте еще раз");
            if (input.InstanceId == Guid.Empty) return Pass("Обновите страницу и попробуйте еще раз");
            if (input.CategoryId == Guid.Empty) return Pass("Не выбрана категория устройства");
            if (input.Specs.Any()) return Pass("Нет спецификаций устройства");
            if (string.IsNullOrEmpty(input.Brand)) return Pass("Нет бренда устройства-");
            if (string.IsNullOrEmpty(input.Model)) return Pass("Нет модели устройства");
            
            var article = new Article(){
                Id = input.NewId,
                InstanceId = input.InstanceId,
                IsDeleted = false,
                Model = input.Model,
                Brand = input.Brand,
                Info = input.Info ?? "",
                CategoryId = input.CategoryId,
            };
            
            await _context.AddAsync(article, cancellationToken);

            if (input.Specs.Any())
            {
                var specs = input.Specs
                    .Select(a =>
                        new Spec()
                        {
                            ArticleId = input.NewId,
                            InstanceId = input.InstanceId,
                            Key = a.Key,
                            Value = a.Value
                        });
                
                await _context.Specs.AddRangeAsync(specs, cancellationToken);
            }

            return Pass(new CreateArticleResultContract());
        }
    }
}