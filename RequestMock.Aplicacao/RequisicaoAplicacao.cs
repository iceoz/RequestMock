using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestMock.Dominio;
using MediatR;
using AutoMapper;

namespace RequestMock.Aplicacao
{
    public class RequisicaoAplicacao : IRequisicaoAplicacao
    {
        IMediator mediator;
        IMapper mapper;

        public RequisicaoAplicacao(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task<Guid> Adicionar(RequisicaoViewModel requisicao)
        {
            var mensagem = mapper.Map<Requisacao>(requisicao);
            mensagem.IdRequisicao = Guid.NewGuid();
            mensagem.Criacao = DateTime.Now;

            var resposta = await mediator.Send(new CadastroRequisicaoAsync(mensagem));
            return resposta;
        }

        public async Task<RequisicaoViewModel> Pesquisar(Guid guidRequisicao)
        {
            var retorno = await mediator.Send(new PesquisaRequisicaoAsync(guidRequisicao));
            return mapper.Map<RequisicaoViewModel>(retorno);
        }
    }
}
