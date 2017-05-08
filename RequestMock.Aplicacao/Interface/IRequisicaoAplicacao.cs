using RequestMock.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestMock.Aplicacao
{
    public interface IRequisicaoAplicacao
    {
        Task<Guid> Adicionar(RequisicaoViewModel requisicao);
        Task<RequisicaoViewModel> Pesquisar(Guid guidRequisicao);
    }
}
