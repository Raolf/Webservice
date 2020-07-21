#pragma checksum "C:\Users\oscar\Documents\GitHub\Webservice\DataWebservice\DataWebservice\Views\API\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "032459afb06b1bf1bc478bf40f55ea6e6b58bec8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_API_Index), @"mvc.1.0.view", @"/Views/API/Index.cshtml")]
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
#line 1 "C:\Users\oscar\Documents\GitHub\Webservice\DataWebservice\DataWebservice\Views\_ViewImports.cshtml"
using DataWebservice;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\oscar\Documents\GitHub\Webservice\DataWebservice\DataWebservice\Views\_ViewImports.cshtml"
using DataWebservice.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"032459afb06b1bf1bc478bf40f55ea6e6b58bec8", @"/Views/API/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd71c099b496fa32c02dc88e77ae77ecfe745705", @"/Views/_ViewImports.cshtml")]
    public class Views_API_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\oscar\Documents\GitHub\Webservice\DataWebservice\DataWebservice\Views\API\Index.cshtml"
  
    ViewData["Title"] = "API";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Api documentation</h1>
<hr />

<h3><a href=""https://datawebservice20200720021920.azurewebsites.net//api/rooms"">/api</a></h3>
<hr />
<h3>Room</h3>

<ul class=""list-group"">
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-info badge-pill"">GET</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/rooms</span>
        </div>
        <div class=""col"">
            <span>Get all rooms</span>
        </div>
    </li>
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-info badge-pill"">GET</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/rooms/{id}</span>
        </div>
        <div class=""col"">
            <span>Get room by ID - includes Owner(s) and SensorData</span>
     ");
            WriteLiteral(@"   </div>
    </li>
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-info badge-pill"">GET</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/room/roomsforuser/{id}</span>
        </div>
        <div class=""col"">
            <span>Get all rooms belonging to User</span>
        </div>
    </li>
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-success badge-pill"">POST</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/room</span>
        </div>
        <div class=""col"">
            <span>Add a room:</span>
            <span>Example:</span>
            <span>{ ""name"" : ""køkken"" }</span>
        </div>
    </li>
    <li class=""list-group-item d-flex justify-content-between a");
            WriteLiteral(@"lign-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-warning badge-pill"">PUT</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/room</span>
        </div>
        <div class=""col"">
            <span>Update an existing room</span>
        </div>
    </li>
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-danger badge-pill"">DELETE</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/room/{id}</span>
        </div>
        <div class=""col"">
            <span>Deletes a room</span>
        </div>
    </li>
</ul>


<hr />
<h3>SensorData</h3>

<ul class=""list-group"">
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-info badge-pill"">GE");
            WriteLiteral(@"T</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/data</span>
        </div>
        <div class=""col"">
            <span>Get all sensorData</span>
        </div>
    </li>

</ul>

<hr />
<h3>User</h3>

<ul class=""list-group"">
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-info badge-pill"">GET</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/users</span>
        </div>
        <div class=""col"">
            <span>Get all users</span>
        </div>
    </li>
    <li class=""list-group-item d-flex justify-content-between align-items-center"">
        <div class=""col-2 col-lg-1"">
            <span class=""badge badge-info badge-pill"">GET</span>
        </div>
        <div class=""col-4 col-lg-2"">
            <span class=""font-weight-bold"">/users/{id}</span>
        </di");
            WriteLiteral("v>\r\n        <div class=\"col\">\r\n            <span>Get user by ID - includes Rooms and Roles</span>\r\n        </div>\r\n    </li>\r\n</ul>\r\n\r\n\r\n");
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
