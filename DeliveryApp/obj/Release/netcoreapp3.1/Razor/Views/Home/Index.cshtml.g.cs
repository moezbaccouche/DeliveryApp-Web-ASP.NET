#pragma checksum "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9d61c28f5d64023afbaeffc5e1056e7d60ca1296"
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
#nullable restore
#line 3 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\_ViewImports.cshtml"
using DeliveryApp.Extensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9d61c28f5d64023afbaeffc5e1056e7d60ca1296", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b039239c523156bb20d4f0ce2e0343c1f090be35", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Moez\source\repos\DeliveryApp\DeliveryApp\Views\Home\Index.cshtml"
  
    ViewBag.CurrentAction = "Index";
    ViewBag.CurrentController = "Dashboard";
    ViewBag.CurrentViewTitle = "Dashboard";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"">
    <div class=""col-lg-3 col-md-6 col-sm-6"">
        <div class=""card card-stats"">
            <div class=""card-body "">
                <div class=""row"">
                    <div class=""col-5 col-md-4"">
                        <div class=""icon-big text-center icon-warning"">
                            <i class=""nc-icon nc-globe text-warning""></i>
                        </div>
                    </div>
                    <div class=""col-7 col-md-8"">
                        <div class=""numbers"">
                            <p class=""card-category"">Capacity</p>
                            <p class=""card-title"">150GB<p>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-footer "">
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-refresh""></i>
                    Update Now
                </div>
            </div>
        </div>
    </div>
    <");
            WriteLiteral(@"div class=""col-lg-3 col-md-6 col-sm-6"">
        <div class=""card card-stats"">
            <div class=""card-body "">
                <div class=""row"">
                    <div class=""col-5 col-md-4"">
                        <div class=""icon-big text-center icon-warning"">
                            <i class=""nc-icon nc-money-coins text-success""></i>
                        </div>
                    </div>
                    <div class=""col-7 col-md-8"">
                        <div class=""numbers"">
                            <p class=""card-category"">Revenue</p>
                            <p class=""card-title"">$ 1,345<p>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-footer "">
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-calendar-o""></i>
                    Last day
                </div>
            </div>
        </div>
    </div>
    <div class=""col-lg-");
            WriteLiteral(@"3 col-md-6 col-sm-6"">
        <div class=""card card-stats"">
            <div class=""card-body "">
                <div class=""row"">
                    <div class=""col-5 col-md-4"">
                        <div class=""icon-big text-center icon-warning"">
                            <i class=""nc-icon nc-vector text-danger""></i>
                        </div>
                    </div>
                    <div class=""col-7 col-md-8"">
                        <div class=""numbers"">
                            <p class=""card-category"">Errors</p>
                            <p class=""card-title"">23<p>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-footer "">
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-clock-o""></i>
                    In the last hour
                </div>
            </div>
        </div>
    </div>
    <div class=""col-lg-3 col-md-6 col-sm-6"">
  ");
            WriteLiteral(@"      <div class=""card card-stats"">
            <div class=""card-body "">
                <div class=""row"">
                    <div class=""col-5 col-md-4"">
                        <div class=""icon-big text-center icon-warning"">
                            <i class=""nc-icon nc-favourite-28 text-primary""></i>
                        </div>
                    </div>
                    <div class=""col-7 col-md-8"">
                        <div class=""numbers"">
                            <p class=""card-category"">Followers</p>
                            <p class=""card-title"">+45K<p>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-footer "">
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-refresh""></i>
                    Update now
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""row"">
    <div class=""col-lg-4 col-sm-6"">
   ");
            WriteLiteral(@"     <div class=""card"">
            <div class=""card-header"">
                <div class=""row"">
                    <div class=""col-sm-7"">
                        <div class=""numbers pull-left"">
                            $34,657
                        </div>
                    </div>
                    <div class=""col-sm-5"">
                        <div class=""pull-right"">
                            <span class=""badge badge-pill badge-success"">
                                +18%
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-body"">
                <h6 class=""big-title"">total earnings in last ten quarters</h6>
                <canvas id=""activeUsers"" width=""826"" height=""380""></canvas>
            </div>
            <div class=""card-footer"">
                <hr>
                <div class=""row"">
                    <div class=""col-sm-7"">
                        <");
            WriteLiteral(@"div class=""footer-title"">Financial Statistics</div>
                    </div>
                    <div class=""col-sm-5"">
                        <div class=""pull-right"">
                            <button class=""btn btn-success btn-round btn-icon btn-sm"">
                                <i class=""nc-icon nc-simple-add""></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-lg-4 col-sm-6"">
        <div class=""card"">
            <div class=""card-header"">
                <div class=""row"">
                    <div class=""col-sm-7"">
                        <div class=""numbers pull-left"">
                            169
                        </div>
                    </div>
                    <div class=""col-sm-5"">
                        <div class=""pull-right"">
                            <span class=""badge badge-pill badge-danger"">
                  ");
            WriteLiteral(@"              -14%
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-body"">
                <h6 class=""big-title"">total subscriptions in last 7 days</h6>
                <canvas id=""emailsCampaignChart"" width=""826"" height=""380""></canvas>
            </div>
            <div class=""card-footer"">
                <hr>
                <div class=""row"">
                    <div class=""col-sm-7"">
                        <div class=""footer-title"">View all members</div>
                    </div>
                    <div class=""col-sm-5"">
                        <div class=""pull-right"">
                            <button class=""btn btn-danger btn-round btn-icon btn-sm"">
                                <i class=""nc-icon nc-button-play""></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
    ");
            WriteLiteral(@"    </div>
    </div>
    <div class=""col-lg-4 col-sm-6"">
        <div class=""card"">
            <div class=""card-header"">
                <div class=""row"">
                    <div class=""col-sm-7"">
                        <div class=""numbers pull-left"">
                            8,960
                        </div>
                    </div>
                    <div class=""col-sm-5"">
                        <div class=""pull-right"">
                            <span class=""badge badge-pill badge-warning"">
                                ~51%
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class=""card-body"">
                <h6 class=""big-title"">total downloads in last 6 years</h6>
                <canvas id=""activeCountries"" width=""826"" height=""380""></canvas>
            </div>
            <div class=""card-footer"">
                <hr>
                <div class=""row"">
       ");
            WriteLiteral(@"             <div class=""col-sm-7"">
                        <div class=""footer-title"">View more details</div>
                    </div>
                    <div class=""col-sm-5"">
                        <div class=""pull-right"">
                            <button class=""btn btn-warning btn-round btn-icon btn-sm"">
                                <i class=""nc-icon nc-alert-circle-i""></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""row"">
    <div class=""col-md-12"">
        <div class=""card "">
            <div class=""card-header "">
                <h4 class=""card-title"">Global Sales by Top Locations</h4>
                <p class=""card-category"">All products that were shipped</p>
            </div>
            <div class=""card-body "">
                <div class=""row"">
                    <div class=""col-md-6"">
                        <div class=""table-");
            WriteLiteral(@"responsive"">
                            <table class=""table"">
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class=""flag"">
                                                <img src=""../assets/img/flags/US.png"">
                                            </div>
                                        </td>
                                        <td>USA</td>
                                        <td class=""text-right"">
                                            2.920
                                        </td>
                                        <td class=""text-right"">
                                            53.23%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class=""flag"">
   ");
            WriteLiteral(@"                                             <img src=""../assets/img/flags/DE.png"">
                                            </div>
                                        </td>
                                        <td>Germany</td>
                                        <td class=""text-right"">
                                            1.300
                                        </td>
                                        <td class=""text-right"">
                                            20.43%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class=""flag"">
                                                <img src=""../assets/img/flags/AU.png"">
                                            </div>
                                        </td>
                                        <td>Australia</td>
             ");
            WriteLiteral(@"                           <td class=""text-right"">
                                            760
                                        </td>
                                        <td class=""text-right"">
                                            10.35%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class=""flag"">
                                                <img src=""../assets/img/flags/GB.png"">
                                            </div>
                                        </td>
                                        <td>United Kingdom</td>
                                        <td class=""text-right"">
                                            690
                                        </td>
                                        <td class=""text-right"">
                                       ");
            WriteLiteral(@"     7.87%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class=""flag"">
                                                <img src=""../assets/img/flags/RO.png"">
                                            </div>
                                        </td>
                                        <td>Romania</td>
                                        <td class=""text-right"">
                                            600
                                        </td>
                                        <td class=""text-right"">
                                            5.94%
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class=""flag"">
      ");
            WriteLiteral(@"                                          <img src=""../assets/img/flags/BR.png"">
                                            </div>
                                        </td>
                                        <td>Brasil</td>
                                        <td class=""text-right"">
                                            550
                                        </td>
                                        <td class=""text-right"">
                                            4.34%
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class=""col-md-6 ml-auto mr-auto"">
                        <div id=""worldMap"" style=""height: 300px;""></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""row"">
    <div class=""co");
            WriteLiteral(@"l-md-6"">
        <div class=""card  card-tasks"">
            <div class=""card-header "">
                <h4 class=""card-title"">Tasks</h4>
                <h5 class=""card-category"">Backend development</h5>
            </div>
            <div class=""card-body "">
                <div class=""table-full-width table-responsive"">
                    <table class=""table"">
                        <tbody>
                            <tr>
                                <td>
                                    <div class=""form-check"">
                                        <label class=""form-check-label"">
                                            <input class=""form-check-input"" type=""checkbox"" checked>
                                            <span class=""form-check-sign""></span>
                                        </label>
                                    </div>
                                </td>
                                <td class=""img-row"">
                                    ");
            WriteLiteral(@"<div class=""img-wrapper"">
                                        <img src=""../assets/img/faces/ayo-ogunseinde-2.jpg"" class=""img-raised"" />
                                    </div>
                                </td>
                                <td class=""text-left"">Sign contract for ""What are conference organizers afraid of?""</td>
                                <td class=""td-actions text-right"">
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 15982, "\"", 15990, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-info btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Edit Task"">
                                        <i class=""nc-icon nc-ruler-pencil""></i>
                                    </button>
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 16291, "\"", 16299, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Remove"">
                                        <i class=""nc-icon nc-simple-remove""></i>
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class=""form-check"">
                                        <label class=""form-check-label"">
                                            <input class=""form-check-input"" type=""checkbox"">
                                            <span class=""form-check-sign""></span>
                                        </label>
                                    </div>
                                </td>
                                <td class=""img-row"">
                                    <div class=""img-wrapper"">
                                        <img src=""../assets/img/faces/erik-lucatero-2.");
            WriteLiteral(@"jpg"" class=""img-raised"" />
                                    </div>
                                </td>
                                <td class=""text-left"">Lines From Great Russian Literature? Or E-mails From My Boss?</td>
                                <td class=""td-actions text-right"">
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 17696, "\"", 17704, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-info btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Edit Task"">
                                        <i class=""nc-icon nc-ruler-pencil""></i>
                                    </button>
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 18005, "\"", 18013, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Remove"">
                                        <i class=""nc-icon nc-simple-remove""></i>
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class=""form-check"">
                                        <label class=""form-check-label"">
                                            <input class=""form-check-input"" type=""checkbox"" checked>
                                            <span class=""form-check-sign""></span>
                                        </label>
                                    </div>
                                </td>
                                <td class=""img-row"">
                                    <div class=""img-wrapper"">
                                        <img src=""../assets/img/faces/kaci-bau");
            WriteLiteral(@"m-2.jpg"" class=""img-raised"" />
                                    </div>
                                </td>
                                <td class=""text-left"">
                                    Using dummy content or fake information in the Web design process can result in products with unrealistic
                                </td>
                                <td class=""td-actions text-right"">
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 19530, "\"", 19538, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-info btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Edit Task"">
                                        <i class=""nc-icon nc-ruler-pencil""></i>
                                    </button>
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 19839, "\"", 19847, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Remove"">
                                        <i class=""nc-icon nc-simple-remove""></i>
                                    </button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class=""form-check"">
                                        <label class=""form-check-label"">
                                            <input class=""form-check-input"" type=""checkbox"">
                                            <span class=""form-check-sign""></span>
                                        </label>
                                    </div>
                                </td>
                                <td class=""img-row"">
                                    <div class=""img-wrapper"">
                                        <img src=""../assets/img/faces/joe-gardner-2.jp");
            WriteLiteral(@"g"" class=""img-raised"" />
                                    </div>
                                </td>
                                <td class=""text-left"">But I must explain to you how all this mistaken idea of denouncing pleasure</td>
                                <td class=""td-actions text-right"">
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 21256, "\"", 21264, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-info btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Edit Task"">
                                        <i class=""nc-icon nc-ruler-pencil""></i>
                                    </button>
                                    <button type=""button"" rel=""tooltip""");
            BeginWriteAttribute("title", " title=\"", 21565, "\"", 21573, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""btn btn-danger btn-round btn-icon btn-icon-mini btn-neutral"" data-original-title=""Remove"">
                                        <i class=""nc-icon nc-simple-remove""></i>
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class=""card-footer "">
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-refresh spin""></i> Updated 3 minutes ago
                </div>
            </div>
        </div>
    </div>
    <div class=""col-md-6"">
        <div class=""card "">
            <div class=""card-header "">
                <h4 class=""card-title"">2020 Sales</h4>
                <p class=""card-category"">All products including Taxes</p>
            </div>
            <div class=""card-body "">
                <canvas id=""chartActivity""></canvas>
            </div>
    ");
            WriteLiteral(@"        <div class=""card-footer "">
                <div class=""legend"">
                    <i class=""fa fa-circle text-info""></i> Tesla Model S
                    <i class=""fa fa-circle text-warning""></i> BMW 5 Series
                </div>
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-check""></i> Data information certified
                </div>
            </div>
        </div>
    </div>
</div>
<div class=""row"">
    <div class=""col-md-3"">
        <div class=""card "">
            <div class=""card-header "">
                <h5 class=""card-title"">Email Statistics</h5>
                <p class=""card-category"">Last Campaign Performance</p>
            </div>
            <div class=""card-body "">
                <canvas id=""chartDonut1"" class=""ct-chart ct-perfect-fourth"" width=""456"" height=""300""></canvas>
            </div>
            <div class=""card-footer "">
                <div class=""legend"">
                    <i class=""fa fa-circle ");
            WriteLiteral(@"text-primary""></i> Open
                </div>
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-calendar""></i> Number of emails sent
                </div>
            </div>
        </div>
    </div>
    <div class=""col-md-3"">
        <div class=""card "">
            <div class=""card-header "">
                <h5 class=""card-title"">New Visitators</h5>
                <p class=""card-category"">Out Of Total Number</p>
            </div>
            <div class=""card-body "">
                <canvas id=""chartDonut2"" class=""ct-chart ct-perfect-fourth"" width=""456"" height=""300""></canvas>
            </div>
            <div class=""card-footer "">
                <div class=""legend"">
                    <i class=""fa fa-circle text-warning""></i> Visited
                </div>
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-check""></i> Campaign sent 2 days ago
                </div>
            </div>
       ");
            WriteLiteral(@" </div>
    </div>
    <div class=""col-md-3"">
        <div class=""card "">
            <div class=""card-header "">
                <h5 class=""card-title"">Orders</h5>
                <p class=""card-category"">Total number</p>
            </div>
            <div class=""card-body "">
                <canvas id=""chartDonut3"" class=""ct-chart ct-perfect-fourth"" width=""456"" height=""300""></canvas>
            </div>
            <div class=""card-footer "">
                <div class=""legend"">
                    <i class=""fa fa-circle text-danger""></i> Completed
                </div>
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-clock-o""></i> Updated 3 minutes ago
                </div>
            </div>
        </div>
    </div>
    <div class=""col-md-3"">
        <div class=""card "">
            <div class=""card-header "">
                <h5 class=""card-title"">Subscriptions</h5>
                <p class=""card-category"">Our Users</p>
            </d");
            WriteLiteral(@"iv>
            <div class=""card-body "">
                <canvas id=""chartDonut4"" class=""ct-chart ct-perfect-fourth"" width=""456"" height=""300""></canvas>
            </div>
            <div class=""card-footer "">
                <div class=""legend"">
                    <i class=""fa fa-circle text-secondary""></i> Ended
                </div>
                <hr>
                <div class=""stats"">
                    <i class=""fa fa-history""></i> Total users
                </div>
            </div>
        </div>
    </div>
</div>

");
            DefineSection("scripts", async() => {
                WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
