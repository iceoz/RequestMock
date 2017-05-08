using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RequestMock.Dominio;
using MongoDB.Driver;
using MongoDB.Bson;

namespace RequestMock.Repositorio
{
    public class RepositorioRequisicao : MongoRepositorio, IRepositorioRequisicao
    {
        readonly string collectionRequisicao = "requisicoes";

        public RepositorioRequisicao(IOptions<MongoConfiguracao> config) : base(config)
        {
            
        }
        
        public async Task<Guid> Adicionar(Requisacao requisicao)
        {
            requisicao._id = ObjectId.GenerateNewId();
            var collection = banco.GetCollection<Requisacao>(collectionRequisicao);
            await collection.InsertOneAsync(requisicao);
            return requisicao.IdRequisicao;
        }

        public async Task<Requisacao> Pesquisar(Guid guidRequisicao)
        {
            var collection = banco.GetCollection<Requisacao>(collectionRequisicao);
            var retorno = await collection.FindAsync(x => x.IdRequisicao == guidRequisicao);
            var requisicao = retorno.FirstOrDefault();
            return requisicao;
        }
    }
}
