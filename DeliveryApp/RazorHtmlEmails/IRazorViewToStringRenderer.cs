using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.RazorHtmlEmails
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
