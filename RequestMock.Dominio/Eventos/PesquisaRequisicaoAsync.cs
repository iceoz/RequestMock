using MediatR;
using RequestMock.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestMock.Dominio
{
    public class PesquisaRequisicaoAsync : IRequest<Requisacao>
    {
        public PesquisaRequisicaoAsync(Guid message)
        {
            this.message = message;
        }

        public Guid message { get; set; }
    }

    public class PesquisaRequisicaoAsyncHandler : IAsyncRequestHandler<PesquisaRequisicaoAsync, Requisacao>
    {
        private IRepositorioRequisicao repositorioRequisicao;

        public PesquisaRequisicaoAsyncHandler(IRepositorioRequisicao repositorioRequisicao)
        {
            this.repositorioRequisicao = repositorioRequisicao;
        }

        public async Task<Requisacao> Handle(PesquisaRequisicaoAsync message)
        {
            return await repositorioRequisicao.Pesquisar(message.message);
        }
    }
}
