using RequestMock.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestMock.Dominio
{
    public interface IRepositorioRequisicao
    {
        Task<Guid> Adicionar(Requisacao requisicao);
        Task<Requisacao> Pesquisar(Guid guidRequisicao);
    }
}
