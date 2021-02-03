using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;
using Nano35.Storage.Processor.Models;
using Nano35.Storage.Processor.Services;

namespace Nano35.Storage.Processor.Configurations
{
    public class AutoMapperConfiguration : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ArticlesBrandAutoMapperProfile());
                mc.AddProfile(new ArticlesCategoryAutoMapperProfile());
                mc.AddProfile(new ArticlesCategoryGroupAutoMapperProfile());
                mc.AddProfile(new ArticlesModelsAutoMapperProfile());
                mc.AddProfile(new ArticleTypesAutoMapperProfile());
                mc.AddProfile(new StorageItemConditionAutoMapperProfile());
                mc.AddProfile(new ArticleAutoMapperProfile());
                mc.AddProfile(new StorageItemAutoMapperProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            MappingPipe.Mapper = mapper;
            services.AddSingleton(mapper);
        }
    }
}