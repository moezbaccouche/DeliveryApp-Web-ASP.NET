#pragma checksum "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7070b59e357ccb49e338ad77553dfdbe37ac7c29"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Client_AllClients), @"mvc.1.0.view", @"/Views/Client/AllClients.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\_ViewImports.cshtml"
using DeliveryApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\_ViewImports.cshtml"
using DeliveryApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\_ViewImports.cshtml"
using DeliveryApp.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7070b59e357ccb49e338ad77553dfdbe37ac7c29", @"/Views/Client/AllClients.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b039239c523156bb20d4f0ce2e0343c1f090be35", @"/Views/_ViewImports.cshtml")]
    public class Views_Client_AllClients : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DeliveryApp.Models.ViewModels.ClientViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
  
    ViewData["Title"] = "AllClients";
    ViewBag.CurrentController = "Client";
    ViewBag.CurrentAction = "AllClients";
    ViewBag.CurrentViewTitle = "Liste des clients";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Liste des clients inscrits</h1>\r\n\r\n<div class=\"container-fluid\">\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 13 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
         foreach (var client in Model.AllClients)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"col-md-4 col-sm-4 col-lg-4\">\r\n            <div class=\"card deliveryMan-card\" style=\"width: 18rem;\">\r\n                <div class=\"text-center deliveryMan-card-header\">\r\n                    <img height=\"100\" width=\"100\"");
            BeginWriteAttribute("src", " src=\"", 633, "\"", 671, 1);
#nullable restore
#line 18 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
WriteAttributeValue("", 639, Url.Content(client.PicturePath), 639, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img rounded-circle img-deliveryMan-card\" alt=\"Image\">\r\n                </div>\r\n                <div class=\"card-body\">\r\n                    <h5 class=\"card-title\">");
#nullable restore
#line 21 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
                                      Write(client.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 21 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
                                                        Write(client.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                    <p class=\"card-text\">Email: ");
#nullable restore
#line 22 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
                                           Write(client.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p class=\"card-text\">Téléphone: ");
#nullable restore
#line 23 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
                                               Write(client.Phone);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <div class=\"float-right\">\r\n                        <a href=\"#\" class=\"btn btn-danger\"><i class=\"fa fa-trash\"></i></a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 30 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Client\AllClients.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DeliveryApp.Models.ViewModels.ClientViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
