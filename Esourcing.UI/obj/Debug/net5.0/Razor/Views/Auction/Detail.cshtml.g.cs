#pragma checksum "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\Auction\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ab42a2df6c721026cd4f7747152b20c16be57042"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Auction_Detail), @"mvc.1.0.view", @"/Views/Auction/Detail.cshtml")]
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
#line 3 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\_ViewImports.cshtml"
using Esourcing.UI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\_ViewImports.cshtml"
using ESourcing.Core.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ab42a2df6c721026cd4f7747152b20c16be57042", @"/Views/Auction/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6228e5b9182e6d6df6d31fd37ec9db850bc98fb0", @"/Views/_ViewImports.cshtml")]
    public class Views_Auction_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AuctionBidModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/microsoft-signalr/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/detail.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\Auction\Detail.cshtml"
  
    ViewData["Title"] = "Detail";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<br />

<div class=""row"">
    <div class=""col-md-6"">
        <div class=""card card-primary"">
            <div class=""card-header"">
                <h3 class=""card-title"">Quick Auction</h3>
            </div>

            <div class=""card-body"">
                <div class=""form-group"">
                    <label for=""exampleInputPrice"">Price</label>
                    <input type=""number"" min=""0"" class=""form-control"" id=""exampleInputPrice"" placeholder=""Enter Price"" />
                </div>
            </div>
            <div class=""card-footer"">
                <button type=""submit"" id=""sendButton"" class=""btn btn-primary"">Submit</button>
");
#nullable restore
#line 24 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\Auction\Detail.cshtml"
                 if (Model.IsAdmin)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <button type=\"submit\" id=\"finishButton\" class=\"btn btn-primary\">Finish RFQ</button>\r\n");
#nullable restore
#line 27 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\Auction\Detail.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n    ");
#nullable restore
#line 31 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\Auction\Detail.cshtml"
Write(Html.HiddenFor(x => x.AuctionId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 32 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\Auction\Detail.cshtml"
Write(Html.HiddenFor(x => x.ProductId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 33 "C:\Users\kucuk\source\repos\ESourcing\Esourcing.UI\Views\Auction\Detail.cshtml"
Write(Html.HiddenFor(x => x.SellerUserName));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <div class=""col-md-6"">
        <div class=""card-header"" style=""background-color:darkgray"">
            <h3 class=""card-title"">Active RFQ Detail</h3>
        </div>

        <div class=""card-body"">
            <table id=""exampleAuction"" class=""table table-bordered table-hover"">
                <thead>
                    <tr style=""background-color:darkgray"">
                        <th>RFQ User Name</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody class=""bidLine"">
                    <tr>
                        <th>Berkay</th>
                        <th>10</th>
                    </tr>

                </tbody>
            </table>
        </div>
    </div>
</div>
<button type=""submit"" id=""finishButton"" class=""btn btn-primary"">Finish RFQ</button>

");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ab42a2df6c721026cd4f7747152b20c16be570427022", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ab42a2df6c721026cd4f7747152b20c16be570428061", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AuctionBidModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
