using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Helpers.TagHelpers
{
    /// <summary>
    /// listeyi alır liste içine dağııtr !   <ul items="@model.list"></ul> bu şekilde kullanılır !!!!!!!!!
    /// </summary>
    [HtmlTargetElement("ul",Attributes="items")] // ul etiketinde kullanılabilir  items olarak atribute verilir 
    public class ListGroupTagHelper : TagHelper
    {
        public List<string> Items { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "list-group");

            foreach (var item in Items)
            {
                TagBuilder li = new TagBuilder("li");

                li.Attributes["class"] = "list-group-item";
                li.InnerHtml.AppendHtml("list-group-item");

                output.PreContent.AppendHtml(li);

            }


        }
    }
}
