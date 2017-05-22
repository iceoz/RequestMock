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
                options.Append($"<option value='{i.Key}' {(primeiroItem ? "selected" : "")}>{i.Value}</option>");
                primeiroItem = false;
            }

            StringBuilder select = new StringBuilder("<select");

            foreach(var attr in context.AllAttributes)
            {
                if (attr.Name != ItensAttributeName)
                    select.Append($" {attr.Name}='{attr.Value}'");
            }

            select.Append($">{options}</select>");

            output.Content.AppendHtml(new HtmlString(select.ToString()));
        }
    }
}
