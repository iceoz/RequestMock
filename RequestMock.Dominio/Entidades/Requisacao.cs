using RequestMock.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RequestMock.Dominio
{
    public class Requisacao
    {
        public object _id { get; set; }
        public Guid IdRequisicao { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ContentType ContentType { get; set; }
        public Encode Encode { get; set; }
        public IEnumerable<Cabecalhos> Cabecalhos { get; set; }
        public String Corpo { get; set; }
        public DateTime Criacao { get; set; }
    }
}
