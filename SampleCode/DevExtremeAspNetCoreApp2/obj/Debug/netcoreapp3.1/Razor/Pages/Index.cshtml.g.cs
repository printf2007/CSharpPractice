#pragma checksum "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9dfdbe8d2686845c1952f503ec71f1d526c347d0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(DevExtremeAspNetCoreApp2.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace DevExtremeAspNetCoreApp2.Pages
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
#line 1 "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\_ViewImports.cshtml"
using DevExtremeAspNetCoreApp2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\_ViewImports.cshtml"
using DevExtreme.AspNet.Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\Index.cshtml"
using DevExtremeAspNetCoreApp2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9dfdbe8d2686845c1952f503ec71f1d526c347d0", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"490ca1ec57a6e80d777cb5d5df42224ef407f5f7", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h2 class=\"content-block\">Home</h2>\r\n\r\n");
#nullable restore
#line 6 "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\Index.cshtml"
Write(Html.DevExtreme().TagBox().DataSource(new string [ ] { "Hello World" , "JK Go go" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\Index.cshtml"
Write(Html.DevExtreme().DataGrid<SampleOrder>()
    .ElementAttr(new { @class = "dx-card wide-card" })
    .DataSource(d => d.Mvc().Controller("SampleData").LoadAction("Get").Key("OrderID"))
    .ShowBorders(false)
    .FilterRow(f => f.Visible(true))
    .FocusedRowEnabled(true)
    .FocusedRowIndex(0)
    .ColumnAutoWidth(true)
    .ColumnHidingEnabled(true)
    .Columns(columns => {
        columns.AddFor(m => m.OrderID);
        columns.AddFor(m => m.OrderDate);
        columns.AddFor(m => m.CustomerName);
        columns.AddFor(m => m.ShipCountry);
        columns.AddFor(m => m.ShipCity);
    })
    .Paging(p => p.PageSize(10))
    .Pager(p => p
        .ShowPageSizeSelector(true)
        .AllowedPageSizes(new[] { 5, 10, 20 })
        .ShowInfo(true)
    )
);

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pages_Index> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_Index> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_Index>)PageContext?.ViewData;
        public Pages_Index Model => ViewData.Model;
    }
}
#pragma warning restore 1591
