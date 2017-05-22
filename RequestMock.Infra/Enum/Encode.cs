using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestMock.Infra
{
    public enum Encode
    {
        [Display(Description = ("UTF-8"))]
        utf8 = 0,

        [Display(Description = ("ISO-8859-1"))]
        iso88591 = 1,

        [Display(Description = ("UTF-16"))]
        utf16 = 2
    }
}
