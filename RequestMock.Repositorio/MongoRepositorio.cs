using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestMock.Repositorio
{
    public class MongoRepositorio
    {
        protected IMongoClient cliente;
        protected IMongoDatabase banco; 

        public MongoRepositorio(IOptions<MongoConfiguracao> config)
        {
            cliente = new MongoClient(config.Value.MongoServer);
            banco = cliente.GetDatabase(config.Value.MongoDataBase);
        }
    }
}
