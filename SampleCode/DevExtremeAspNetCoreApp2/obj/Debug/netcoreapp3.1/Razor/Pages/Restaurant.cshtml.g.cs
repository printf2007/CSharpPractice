#pragma checksum "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\Restaurant.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be35a0a86f91cdad324b60f3507861fcb28fef0f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(DevExtremeAspNetCoreApp2.Pages.Pages_Restaurant), @"mvc.1.0.razor-page", @"/Pages/Restaurant.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be35a0a86f91cdad324b60f3507861fcb28fef0f", @"/Pages/Restaurant.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"490ca1ec57a6e80d777cb5d5df42224ef407f5f7", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Restaurant : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\Restaurant.cshtml"
  
    ViewData["Title"] = "Restaurant";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h2>Restaurant</h2>\r\n\r\n");
#nullable restore
#line 8 "C:\MyGit\CSharpPractice\SampleCode\DevExtremeAspNetCoreApp2\Pages\Restaurant.cshtml"
Write(Html.DevExtreme().DataGrid<DevExtremeAspNetCoreApp2.Models.Restaurants>()
    .DataSource(ds => ds.Mvc()
        .Controller("Restaurants")
        .LoadAction("Get")
        .InsertAction("Post")
        .UpdateAction("Put")
        .DeleteAction("Delete")
        .Key("Id")
    )
    .RemoteOperations(true)
    .Columns(columns => {

        columns.AddFor(m => m.Id);

        columns.AddFor(m => m.Name);

        columns.AddFor(m => m.Location);

        columns.AddFor(m => m.Cuisine);
    })
    .Editing(e => e
        .AllowAdding(true)
        .AllowUpdating(true)
        .AllowDeleting(true)
    )
);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Pages_Restaurant> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_Restaurant> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Pages_Restaurant>)PageContext?.ViewData;
        public Pages_Restaurant Model => ViewData.Model;
    }
}
#pragma warning restore 1591
