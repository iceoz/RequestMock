using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RequestMock.Aplicacao;
using RequestMock.Infra;
using System;
using RequestMock.Infra.Helper;

namespace RequestMock.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        IRequisicaoAplicacao _aplicacao;

        
        public HomeController(IRequisicaoAplicacao aplicacao)
        {
            _aplicacao = aplicacao;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            await CarregarViewState();
            return View(); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Pesquisar(string id)
        {
            var retorno = await PesquisarPorID(id);
            if (retorno != null)
                return new ContentResult() { Content = retorno.Corpo, ContentType = retorno.ContentType.ObterDescricao(), StatusCode = (int)retorno.StatusCode };
            return BadRequest("Não foi possivel encontrar a chamada");
        }

        [HttpGet("{id}/{callback}")]
        public async Task<IActionResult> PesquisarComCallback(string id, string callback)
        {
            var retorno = await PesquisarPorID(id);
            if (retorno != null && !String.IsNullOrEmpty(callback))
            {
                retorno.Corpo = string.Format("function {0}() { return '{1}'; }", callback, HelperString.Base64Encode(retorno.Corpo, retorno.Encode));
                return new ContentResult() { Content = retorno.Corpo, ContentType = "application/javascript", StatusCode = (int)retorno.StatusCode };
            }
            return BadRequest("Não foi possivel encontrar a chamada");
        }

        private async Task<RequisicaoViewModel> PesquisarPorID(string id)
        {
            if (Guid.TryParse(id, out Guid guid))
            {
                return await _aplicacao.Pesquisar(guid);
            }

            return null;
        }        

        //[HttpPost("gravar")]
        [HttpGet("gravar")]
        public async Task<IActionResult> Gravar()
        {
            var chamada = new RequisicaoViewModel() { ContentType = ContentType.textJson, Encode = Encode.utf16, StatusCode = System.Net.HttpStatusCode.OK, Corpo = "{ \"a\" : \"b\", \"b\" : \"c\" }" };

            var resposta = await _aplicacao.Adicionar(chamada);

            var tralal = await _aplicacao.Pesquisar(resposta);

            return Ok(resposta);
        }

        private async Task CarregarViewState()
        {
            ViewBag.Encode = await HelperEnum.ObterDescricaoEnconde();
            ViewBag.ContentType = await HelperEnum.ObterDescricaoContentType();
            ViewBag.StatusCode = await HelperEnum.ObterDescricaoStatusCode();
        }

    }
}
