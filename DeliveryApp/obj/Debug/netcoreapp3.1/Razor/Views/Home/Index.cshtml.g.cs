#pragma checksum "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee6831a588f3ee34fe82e83e61f89c96d4eb93f3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee6831a588f3ee34fe82e83e61f89c96d4eb93f3", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a8c17bd6bc47298a47a1f0d59f587625e92904df", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Home\Index.cshtml"
  
    Layout = "_Layout";
    ViewBag.CurrentAction = "Dashboard";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""content"">
    <div class=""container-fluid"">
        <div class=""row"">
            <div class=""col-lg-3 col-md-6 col-sm-6"">
                <div class=""card card-stats"">
                    <div class=""card-header card-header-warning card-header-icon"">
                        <div class=""card-icon"">
                            <i class=""material-icons"">content_copy</i>
                        </div>
                        <p class=""card-category"">Used Space</p>
                        <h3 class=""card-title"">
                            49/50
                            <small>GB</small>
                        </h3>
                    </div>
                    <div class=""card-footer"">
                        <div class=""stats"">
                            <i class=""material-icons text-danger"">warning</i>
                            <a href=""#pablo"">Get More Space...</a>
                        </div>
                    </div>
                </div>
            </div>
   ");
            WriteLiteral(@"         <div class=""col-lg-3 col-md-6 col-sm-6"">
                <div class=""card card-stats"">
                    <div class=""card-header card-header-success card-header-icon"">
                        <div class=""card-icon"">
                            <i class=""material-icons"">store</i>
                        </div>
                        <p class=""card-category"">Revenue</p>
                        <h3 class=""card-title"">$34,245</h3>
                    </div>
                    <div class=""card-footer"">
                        <div class=""stats"">
                            <i class=""material-icons"">date_range</i> Last 24 Hours
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-lg-3 col-md-6 col-sm-6"">
                <div class=""card card-stats"">
                    <div class=""card-header card-header-danger card-header-icon"">
                        <div class=""card-icon"">
                            <i c");
            WriteLiteral(@"lass=""material-icons"">info_outline</i>
                        </div>
                        <p class=""card-category"">Fixed Issues</p>
                        <h3 class=""card-title"">75</h3>
                    </div>
                    <div class=""card-footer"">
                        <div class=""stats"">
                            <i class=""material-icons"">local_offer</i> Tracked from Github
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-lg-3 col-md-6 col-sm-6"">
                <div class=""card card-stats"">
                    <div class=""card-header card-header-info card-header-icon"">
                        <div class=""card-icon"">
                            <i class=""fa fa-twitter""></i>
                        </div>
                        <p class=""card-category"">Followers</p>
                        <h3 class=""card-title"">+245</h3>
                    </div>
                    <div class=""card-foote");
            WriteLiteral(@"r"">
                        <div class=""stats"">
                            <i class=""material-icons"">update</i> Just Updated
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-md-4"">
                <div class=""card card-chart"">
                    <div class=""card-header card-header-success"">
                        <div class=""ct-chart"" id=""dailySalesChart""></div>
                    </div>
                    <div class=""card-body"">
                        <h4 class=""card-title"">Daily Sales</h4>
                        <p class=""card-category"">
                            <span class=""text-success""><i class=""fa fa-long-arrow-up""></i> 55% </span> increase in today sales.
                        </p>
                    </div>
                    <div class=""card-footer"">
                        <div class=""stats"">
                            <i class=""material-icons"">a");
            WriteLiteral(@"ccess_time</i> updated 4 minutes ago
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-md-4"">
                <div class=""card card-chart"">
                    <div class=""card-header card-header-warning"">
                        <div class=""ct-chart"" id=""websiteViewsChart""></div>
                    </div>
                    <div class=""card-body"">
                        <h4 class=""card-title"">Email Subscriptions</h4>
                        <p class=""card-category"">Last Campaign Performance</p>
                    </div>
                    <div class=""card-footer"">
                        <div class=""stats"">
                            <i class=""material-icons"">access_time</i> campaign sent 2 days ago
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-md-4"">
                <div class=""card card-chart"">
                    <div c");
            WriteLiteral(@"lass=""card-header card-header-danger"">
                        <div class=""ct-chart"" id=""completedTasksChart""></div>
                    </div>
                    <div class=""card-body"">
                        <h4 class=""card-title"">Completed Tasks</h4>
                        <p class=""card-category"">Last Campaign Performance</p>
                    </div>
                    <div class=""card-footer"">
                        <div class=""stats"">
                            <i class=""material-icons"">access_time</i> campaign sent 2 days ago
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class=""row"">
            <div class=""col-lg-6 col-md-12"">
                <div class=""card"">
                    <div class=""card-header card-header-tabs card-header-primary"">
                        <div class=""nav-tabs-navigation"">
                            <div class=""nav-tabs-wrapper"">
                                <sp");
            WriteLiteral(@"an class=""nav-tabs-title"">Tasks:</span>
                                <ul class=""nav nav-tabs"" data-tabs=""tabs"">
                                    <li class=""nav-item"">
                                        <a class=""nav-link active"" href=""#profile"" data-toggle=""tab"">
                                            <i class=""material-icons"">bug_report</i> Bugs
                                            <div class=""ripple-container""></div>
                                        </a>
                                    </li>
                                    <li class=""nav-item"">
                                        <a class=""nav-link"" href=""#messages"" data-toggle=""tab"">
                                            <i class=""material-icons"">code</i> Website
                                            <div class=""ripple-container""></div>
                                        </a>
                                    </li>
                                    <li class=""nav-item"">
         ");
            WriteLiteral(@"                               <a class=""nav-link"" href=""#settings"" data-toggle=""tab"">
                                            <i class=""material-icons"">cloud</i> Server
                                            <div class=""ripple-container""></div>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class=""card-body"">
                        <div class=""tab-content"">
                            <div class=""tab-pane active"" id=""profile"">
                                <table class=""table"">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                  ");
            WriteLiteral("                                      <input class=\"form-check-input\" type=\"checkbox\"");
            BeginWriteAttribute("value", " value=\"", 8351, "\"", 8359, 0);
            EndWriteAttribute();
            WriteLiteral(@" checked>
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Sign contract for ""What are conference organizers afraid of?""</td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                                                </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-d");
            WriteLiteral(@"anger btn-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 10014, "\"", 10022, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Lines From Great Russian Literature? Or E-mails From My Boss?</td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                                                </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-danger bt");
            WriteLiteral(@"n-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 11669, "\"", 11677, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit
                                            </td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                               ");
            WriteLiteral(@"                 </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-danger btn-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 13475, "\"", 13483, 0);
            EndWriteAttribute();
            WriteLiteral(@" checked>
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Create 4 Invisible User Experiences you Never Knew About</td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                                                </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-danger");
            WriteLiteral(@" btn-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class=""tab-pane"" id=""messages"">
                                <table class=""table"">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 15423, "\"", 15431, 0);
            EndWriteAttribute();
            WriteLiteral(@" checked>
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit
                                            </td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                       ");
            WriteLiteral(@"                         </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-danger btn-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 17237, "\"", 17245, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Sign contract for ""What are conference organizers afraid of?""</td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                                                </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-danger bt");
            WriteLiteral(@"n-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class=""tab-pane"" id=""settings"">
                                <table class=""table"">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 19182, "\"", 19190, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Lines From Great Russian Literature? Or E-mails From My Boss?</td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                                                </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-danger bt");
            WriteLiteral(@"n-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 20837, "\"", 20845, 0);
            EndWriteAttribute();
            WriteLiteral(@" checked>
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>
                                                Flooded: One year later, assessing what was lost and what was found when a ravaging rain swept through metro Detroit
                                            </td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                       ");
            WriteLiteral(@"                         </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-danger btn-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class=""form-check"">
                                                    <label class=""form-check-label"">
                                                        <input class=""form-check-input"" type=""checkbox""");
            BeginWriteAttribute("value", " value=\"", 22651, "\"", 22659, 0);
            EndWriteAttribute();
            WriteLiteral(@" checked>
                                                        <span class=""form-check-sign"">
                                                            <span class=""check""></span>
                                                        </span>
                                                    </label>
                                                </div>
                                            </td>
                                            <td>Sign contract for ""What are conference organizers afraid of?""</td>
                                            <td class=""td-actions text-right"">
                                                <button type=""button"" rel=""tooltip"" title=""Edit Task"" class=""btn btn-primary btn-link btn-sm"">
                                                    <i class=""material-icons"">edit</i>
                                                </button>
                                                <button type=""button"" rel=""tooltip"" title=""Remove"" class=""btn btn-d");
            WriteLiteral(@"anger btn-link btn-sm"">
                                                    <i class=""material-icons"">close</i>
                                                </button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""col-lg-6 col-md-12"">
                <div class=""card"">
                    <div class=""card-header card-header-warning"">
                        <h4 class=""card-title"">Employees Stats</h4>
                        <p class=""card-category"">New employees on 15th September, 2016</p>
                    </div>
                    <div class=""card-body table-responsive"">
                        <table class=""table table-hover"">
                            <thead class=""text-warning"">
        ");
            WriteLiteral(@"                    <th>ID</th>
                            <th>Name</th>
                            <th>Salary</th>
                            <th>Country</th>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <td>Dakota Rice</td>
                                    <td>$36,738</td>
                                    <td>Niger</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>Minerva Hooper</td>
                                    <td>$23,789</td>
                                    <td>Curaçao</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>Sage Rodriguez</td>
                                    <td>$56,142</td>");
            WriteLiteral(@"
                                    <td>Netherlands</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>Philip Chaney</td>
                                    <td>$38,735</td>
                                    <td>Korea, South</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
