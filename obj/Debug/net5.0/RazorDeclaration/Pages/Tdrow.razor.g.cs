// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Blazorvocabulary.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Blazorvocabulary;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Owner\Documents\projects\Blazorvocabulary\_Imports.razor"
using Blazorvocabulary.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Owner\Documents\projects\Blazorvocabulary\Pages\TdRow.razor"
using Collins;

#line default
#line hidden
#nullable disable
    public partial class TdRow : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 62 "C:\Users\Owner\Documents\projects\Blazorvocabulary\Pages\TdRow.razor"
       
    private TextEntry tr;

    private string htmlclass="d-inline p-2 bg-primary text-white";

    [Parameter]
    public TextEntry Translation
    {
        get { return tr; }
        set { tr = value;
        
        if(tr.Entry=="crumple")
        htmlclass="d-inline p-2 bg-info text-white";
        
        
         }
    }



#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
