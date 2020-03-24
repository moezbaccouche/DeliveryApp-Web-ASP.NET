using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Extensions
{
    public static class HtmlExtensions
    {
            public static HtmlString Script(this IHtmlHelper htmlHelper, Func<object, HelperResult> template)
            {
                htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
                return HtmlString.Empty;
            }

            public static HtmlString RenderScripts(this IHtmlHelper htmlHelper)
            {
                foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
                {
                    if (key.ToString().StartsWith("_script_"))
                    {
                        var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                        if (template != null)
                        {
                            htmlHelper.ViewContext.Writer.Write(template(null));
                        }
                    }
                }
                return HtmlString.Empty;
            }
        
    }
}
