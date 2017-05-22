using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RequestMock.Aplicacao;
using RequestMock.Infra;
using System;
using System.Linq;
using RequestMock.Infra.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

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
            await CarregarViewBag(null);

            return View(new RequisicaoViewModel() { StatusCode = System.Net.HttpStatusCode.OK });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Pesquisar(string id)
        {
            var retorno = await PesquisarPorID(id);
            if (retorno != null)
            {
                return new ContentResult() { Content = retorno.Requestcontent, ContentType = retorno.ContentType.ObterDescricao(), StatusCode = (int)retorno.StatusCode };
            }
            return BadRequest("Não foi possivel encontrar a chamada");
        }

        [HttpGet("{id}/{callback}")]
        public async Task<IActionResult> PesquisarComCallback(string id, string callback)
        {
            var retorno = await PesquisarPorID(id);
            if (retorno != null && !String.IsNullOrEmpty(callback))
            {
                retorno.Requestcontent = string.Format("function {0}() { return '{1}'; }", callback, HelperString.Base64Encode(retorno.Requestcontent, retorno.Encode));
                return new ContentResult() { Content = retorno.Requestcontent, ContentType = "application/javascript", StatusCode = (int)retorno.StatusCode };
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

        [HttpPost("gravar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gravar(RequisicaoViewModel formData)
        {
            try
            {
                foreach (var e in _aplicacao.ValidarRequisicao(formData))
                {
                    ModelState.AddModelError("", e);
                }

                if (ModelState.IsValid)
                {
                    formData.Chave = await _aplicacao.Adicionar(formData);
                }
            }
            catch
            {
                formData.Chave = Guid.Empty;
            }

            await CarregarViewBag(formData);

            return View("Index", formData);
        }

        private async Task CarregarViewBag(RequisicaoViewModel formData)
        {
            await Task.WhenAll(new Task[] {
                CarregarViewBagHeaders(formData),
                MontarComboEncode(formData),
                MontarComboContentType(formData),
                MontarComboStatusCode(formData)
            });
        }

        private async Task CarregarViewBagHeaders(RequisicaoViewModel formData)
        {
            await Task.Run(() =>
            {
                ViewBag.MostrarHeaders = formData != null && (
                    formData.HeaderNames.Any(x => !string.IsNullOrEmpty(x)) ||
                    formData.HeaderValues.Any(x => !string.IsNullOrEmpty(x))
                );
            });
        }

        private async Task MontarComboEncode(RequisicaoViewModel formData)
        {
            var dicionario = await HelperEnum.ObterDescricaoEnconde();
            int? valor = null;
            if (formData != null)
            {
                valor = (int)formData.Encode;
            }

            ViewBag.Encode = await MontarCombo(dicionario, valor);
        }

        private async Task MontarComboStatusCode(RequisicaoViewModel formData)
        {
            var dicionario = await HelperEnum.ObterDescricaoStatusCode();
            int? valor = null;
            if (formData != null)
            {
                valor = (int)formData.StatusCode;
            }

            ViewBag.StatusCode = await MontarCombo(dicionario, valor);
        }

        private async Task MontarComboContentType(RequisicaoViewModel formData)
        {
            var dicionario = await HelperEnum.ObterDescricaoContentType();
            int? valor = null;
            if (formData != null)
            {
                valor = (int)formData.ContentType;
            }

            ViewBag.ContentType = await MontarCombo(dicionario, valor);
        }

        private async Task<IEnumerable<SelectListItem>> MontarCombo(IDictionary<int, string> dicionario, int? valor = null)
        {
            IList<SelectListItem> retorno = await Task.Run(() =>
            {
                ConcurrentBag<SelectListItem> d = new ConcurrentBag<SelectListItem>();

                dicionario.AsParallel().ForAll(x => {
                    d.Add(new SelectListItem()
                    {
                        Value = x.Key.ToString(),
                        Text = x.Value,
                        Selected = valor.HasValue && x.Key == valor.Value
                    });
                });

                return d.ToList();
            });

            return retorno;
        }

    }
}
