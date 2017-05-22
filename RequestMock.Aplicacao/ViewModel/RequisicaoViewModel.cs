using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RequestMock.Infra;
using System.ComponentModel.DataAnnotations;

namespace RequestMock.Aplicacao
{
    public class RequisicaoViewModel
    {
        [Required]
        public ContentType ContentType { get; set; }

        [Required]
        public string Requestcontent { get; set; }

        [Required]
        public Encode Encode { get; set; }

        [Required]
        public HttpStatusCode StatusCode { get; set; }

        public IList<string> HeaderNames { get; set; }

        public IList<string> HeaderValues { get; set; }

        public Guid? Chave { get; set; }
    }
}
