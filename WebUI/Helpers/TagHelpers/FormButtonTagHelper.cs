using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Helpers.TagHelpers
{

    /// <summary>
    /// <formbutton type="Submit" BgColor="primary"> </formbutton> // bu şekilde çağrılabilir tpye ve bg coloru yazmasanda otomatik string olduğu için submit ve primary atar !!!!
    /// </summary>
    [HtmlTargetElement("formbutton")] // formbutton etiket tanımlar 
    public class FormButtonTagHelper : TagHelper
    {
        public string Type { get; set; } = "Submit";
        public string BgColor { get; set; } = "primary";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button"; // etiket adı 
            output.TagMode = TagMode.StartTagAndEndTag; // etiketin açlışını ve kapanışını yap 
            output.Attributes.SetAttribute("class", $"btn btn-{BgColor}"); // class atribute
            output.Attributes.SetAttribute("type", Type); // type attribitu
            output.Content.SetContent(Type == "submit" ? "add" : "reset"); // buton adı yani buton etiketinin içeriği
                

        }
    }
}
