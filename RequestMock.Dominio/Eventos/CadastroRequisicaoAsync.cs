using MediatR;
using RequestMock.Dominio;
using System;
using System.Threading.Tasks;

namespace RequestMock.Dominio
{
    public class CadastroRequisicaoAsync : IRequest<Guid>
    {
        public CadastroRequisicaoAsync(Requisacao message)
        {
            this.message = message;
        }

        public Requisacao message { get; set; }
    }

    public class CadastroRequisicaoAsyncHandler : IAsyncRequestHandler<CadastroRequisicaoAsync, Guid>
    {
        private IRepositorioRequisicao repositorioRequisicao;

        public CadastroRequisicaoAsyncHandler(IRepositorioRequisicao repositorioRequisicao)
        {
            this.repositorioRequisicao = repositorioRequisicao;
        }

        public async Task<Guid> Handle(CadastroRequisicaoAsync message)
        {
            return await repositorioRequisicao.Adicionar(message.message);
        }
    }
}
