using System.ComponentModel.DataAnnotations;

namespace RequestMock.Infra
{
    public enum ContentType
    {
        [Display(Description = ("application/json"))]
        applicationJson = 0,

        [Display(Description = ("application/x-www-form-urlencoded"))]
        applicationXWwwFormUrlEncoded = 1,

        [Display(Description = ("application/xhtml-xml"))]
        applicationXhml = 2,

        [Display(Description = ("application/xml"))]
        applicationXml = 3,

        [Display(Description = ("multipart/form-data"))]
        multipartFormData = 4,

        [Display(Description = ("text/css"))]
        textCss = 5,

        [Display(Description = ("text/csv"))]
        textCsv = 6,

        [Display(Description = ("text/html"))]
        textHtml = 7,

        [Display(Description = ("text/json"))]
        textJson = 8,

        [Display(Description = ("text/plain"))]
        textPlain = 9,

        [Display(Description = ("text/xml"))]
        textXml = 10
    }
}
