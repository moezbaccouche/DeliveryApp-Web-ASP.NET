#pragma checksum "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e5052183f1a89bc7a716962d851710eb586fa655"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Category_AllCategories), @"mvc.1.0.view", @"/Views/Category/AllCategories.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e5052183f1a89bc7a716962d851710eb586fa655", @"/Views/Category/AllCategories.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b039239c523156bb20d4f0ce2e0343c1f090be35", @"/Views/_ViewImports.cshtml")]
    public class Views_Category_AllCategories : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DeliveryApp.Models.ViewModels.CategoryViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
  
    ViewData["Title"] = "AllCategories";
    ViewBag.CurrentController = "Category";
    ViewBag.CurrentAction = "AllCategories";
    ViewBag.CurrentView = "Catégories";
    ViewBag.CurrentViewTitle = "Liste des catégories";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container-fluid\">\r\n\r\n");
#nullable restore
#line 12 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
     if (TempData["Message"] != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""alert alert-success alert-dismissible fade show"">
            <button type=""button"" aria-hidden=""true"" class=""close"" data-dismiss=""alert"" aria-label=""Close"">
                <i class=""nc-icon nc-simple-remove""></i>
            </button>
            <span>");
#nullable restore
#line 18 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
             Write(TempData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n        </div>\r\n");
#nullable restore
#line 20 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <h1>Toutes les catégories</h1>\r\n\r\n\r\n\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 27 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
         foreach (var category in Model.Categories)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-4 col-sm-4 col-lg-4\">\r\n                <div class=\"card\" style=\"width: 18rem; height: 350px\">\r\n                    <img height=\"180\"");
            BeginWriteAttribute("src", " src=\"", 999, "\"", 1037, 1);
#nullable restore
#line 31 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
WriteAttributeValue("", 1005, Url.Content(category.ImagePath), 1005, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\" alt=\"Image\">\r\n                    <div class=\"card-body\">\r\n                        <h5 class=\"card-title\">");
#nullable restore
#line 33 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
                                          Write(category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        <p class=\"card-text\">");
#nullable restore
#line 34 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
                                        Write(category.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <div class=\"float-right\">\r\n                            <a href=\"#\" class=\"btn btn-info btn-simple btn-icon ajaxBtn\" data-link=\"/Category/EditCategory?id=");
#nullable restore
#line 36 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
                                                                                                                         Write(category.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""" data-title=""Modifier Catégorie""><i class=""fa fa-pencil""></i></a>
                            <a href=""#"" class=""btn btn-danger btn-simple btn-icon""><i class=""fa fa-trash""></i></a>
                        </div>
                    </div>
                </div>
            </div>
");
#nullable restore
#line 42 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Category\AllCategories.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n\r\n</div>\r\n\r\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
    <script>
        function pictureAdded(e) {
            var reader = new FileReader();
            reader.onload = function () {
                var output = document.getElementById('imgCat');
                output.src = reader.result;
                console.log(reader.result);
            };
            reader.readAsDataURL(e.target.files[0]);
        }

    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DeliveryApp.Models.ViewModels.CategoryViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
