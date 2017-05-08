using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestMock.Infra
{
    public class MongoConfiguration
    {
        public string MongoServer { get; set; }
        public string MongoDataBase { get; set; }
    }
}
