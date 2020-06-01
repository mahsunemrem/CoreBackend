using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Helpers.TagHelpers
{
    /// <summary>
    /// bs-button-color="warning" şejkşinde çağrılır
    /// </summary>
    [HtmlTargetElement("button", Attributes = "bs-button-color", ParentTag = "form")] // çağırma şeklinin adını verdik  form içinde çağrılabilir ve button nesnesinde 
    public class ButtonTagHelper : TagHelper
    {     
        public string BsButtonColor { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", $"btn btn-{BsButtonColor}");
        }
    }
}
