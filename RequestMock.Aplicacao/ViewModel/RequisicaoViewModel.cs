using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RequestMock.Infra;

namespace RequestMock.Aplicacao
{
    public class RequisicaoViewModel
    {
        public ContentType ContentType { get; set; }
        public string Corpo { get; set; }
        public Encode Encode { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
