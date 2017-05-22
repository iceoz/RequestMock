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
            ValidarRequisicao(requisicao);

            var mensagem = mapper.Map<Requisacao>(requisicao);
            mensagem.IdRequisicao = Guid.NewGuid();
            mensagem.Criacao = DateTime.Now;

            var resposta = await mediator.Send(new CadastroRequisicaoAsync(mensagem));
            return resposta;
        }

        public IList<string> ValidarRequisicao(RequisicaoViewModel requisicao)
        {
            var erros = new List<string>();
            int titulosEmBranco = 0;                       

            if(requisicao != null && requisicao.HeaderNames != null && requisicao.HeaderValues != null)
            {
                for(int i = 0; i < requisicao.HeaderNames.Count(); i++)
                {
                    if (string.IsNullOrEmpty(requisicao.HeaderNames[i]) && !string.IsNullOrEmpty(requisicao.HeaderValues[i]))
                    {
                        titulosEmBranco++;
                    }
                    else if (!string.IsNullOrEmpty(requisicao.HeaderNames[i]) && string.IsNullOrEmpty(requisicao.HeaderValues[i]))
                    {
                        erros.Add($"Valor de {requisicao.HeaderNames[i]} está em branco");
                    }                        
                }

                if(titulosEmBranco > 0)
                {
                    if(titulosEmBranco > 1)
                    {
                        erros.Insert(0, $"Foram encontrado {titulosEmBranco} headers com título em branco");
                    }
                    else
                    {
                        erros.Insert(0, "Foi encontrado 1 header com título em branco");
                    }                   
                }
            }

            return erros;
        }

        public async Task<RequisicaoViewModel> Pesquisar(Guid guidRequisicao)
        {
            var retorno = await mediator.Send(new PesquisaRequisicaoAsync(guidRequisicao));
            return mapper.Map<RequisicaoViewModel>(retorno);
        }
    }
}
