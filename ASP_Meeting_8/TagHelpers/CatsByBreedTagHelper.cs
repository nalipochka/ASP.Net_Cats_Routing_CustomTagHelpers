using ASP_Meeting_8.Models.DTO;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ASP_Meeting_8.TagHelpers
{
    public class CatsByBreedTagHelper : TagHelper
    {
        private readonly LinkGenerator linkGenerator;

        public IEnumerable<BreedDTO> Breeds { get; set; } = default!;
        public string CurrentPage { get; set; } = default!;
        public string AspController { get; set; } = default!;
        public string AspAction { get; set; } = default!;
        public CatsByBreedTagHelper(LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "btn-group");
            string? href = linkGenerator.GetPathByAction(AspAction, AspController, new {breed = string.Empty});
            StringBuilder sb = new();
            if (!string.IsNullOrEmpty(CurrentPage))
            {
                sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-secondary\">All</a>");
            }
            else
            {
                sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-primary\">All</a>");
            }
            foreach (BreedDTO breed in Breeds)
            {
                href = linkGenerator.GetPathByAction(AspAction, AspController, new { breed = breed.BreedName });
                if(breed.BreedName == CurrentPage)
                {
                    sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-primary\">{breed.BreedName}</a>");
                }
                else
                {
                    sb.Append($"<a href=\"{href}\" class=\"btn btn-sm btn-outline-secondary\">{breed.BreedName}</a>");
                }
            }
            output.Content.SetHtmlContent( sb.ToString() );
        }
    }
}
