using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RequestMock.Repositorio;
using MediatR;
using RequestMock.Dominio;
using AutoMapper;
using RequestMock.Aplicacao;

namespace RequestMock.Cross
{
    public static class Ioc
    {
        public static void Configurar(IConfigurationRoot configuration, IServiceCollection services)
        {
            ConfigurarOptions(configuration, services);
            ConfigurarMediatR(services);
            ConfigurarAutoMapper(services);
            ConfigurarAplicacao(services);
            ConfigurarRepositorios(services);
        }

        private static void ConfigurarOptions(IConfigurationRoot configuration, IServiceCollection services)
        {
            services.Configure<MongoConfiguracao>(configuration.GetSection("MongoConfiguracao"));
        }

        private static void ConfigurarAplicacao(IServiceCollection services)
        {
            services.AddScoped<IRequisicaoAplicacao, RequisicaoAplicacao>();
        }

        private static void ConfigurarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IRepositorioRequisicao, RepositorioRequisicao>();
        }

        private static void ConfigurarMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(CadastroRequisicaoAsyncHandler));
            services.AddMediatR(typeof(PesquisaRequisicaoAsyncHandler));
        }

        private static void ConfigurarAutoMapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(x => {
                x.CreateMap<Requisacao, RequisicaoViewModel>().ReverseMap();
            });
            
            services.AddSingleton(config.CreateMapper());            
        }
    }
}
