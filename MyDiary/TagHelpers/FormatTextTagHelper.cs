using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyDiary.TagHelpers
{
    [HtmlTargetElement("format-text")]
    [HtmlTargetElement(Attributes = "format-text")]
    public class FormatTextTagHelper : TagHelper
    {
        public async override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var TextToSplit = (await output.GetChildContentAsync()).GetContent();
            var decoded = HttpUtility.HtmlDecode(TextToSplit);
            output.Attributes.RemoveAll("format-text");
            output.Content.SetHtmlContent(decoded);
        }
    }
}
