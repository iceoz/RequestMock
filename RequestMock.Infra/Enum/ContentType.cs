using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RequestMock.Infra
{
    public enum ContentType
    {
        [Description("application/json")]
        applicationJson = 0,

        [Description("application/x-www-form-urlencoded")]
        applicationXWwwFormUrlEncoded = 1,

        [Description("application/xhtml-xml")]
        applicationXhml = 2,

        [Description("application/xml")]
        applicationXml = 3,

        [Description("multipart/form-data")]
        multipartFormData = 4,

        [Description("text/css")]
        textCss = 5,

        [Description("text/csv")]
        textCsv = 6,

        [Description("text/html")]
        textHtml = 7,

        [Description("text/json")]
        textJson = 8,

        [Description("text/plain")]
        textPlain = 9,

        [Description("text/xml")]
        textXml = 10
    }
}
