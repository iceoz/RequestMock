using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestMock.Web
{
    [HtmlTargetElement("select", Attributes = ItensAttributeName)]
    public class SelectDictionaryTagHelper : TagHelper
    {
        private const string ItensAttributeName = "dicionario";

        [HtmlAttributeName(ItensAttributeName)]
        public IDictionary<int, string> Itens { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            CriarCombo(context, output);

            base.Process(context, output);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await Task.Run(() => { CriarCombo(context, output); });            
        }

        private void CriarCombo(TagHelperContext context, TagHelperOutput output)
        {
            output.SuppressOutput();

            bool primeiroItem = true;

            StringBuilder options = new StringBuilder();
                        
            foreach (var i in Itens)
            {
                options.Append(string.Format("<option value='{0}' {1}>{2}</option>", i.Key, primeiroItem ? "selected" : "", i.Value));

                primeiroItem = false;
            }

            string selectContent = $@"<select>"+options.ToString()+"</select>";

            output.Content.AppendHtml(new HtmlString(selectContent));
        }
    }
}
