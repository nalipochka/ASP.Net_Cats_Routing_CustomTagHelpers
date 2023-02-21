using ASP_Meeting_8.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_Meeting_8.Models.ViewModels.CatViewModels
{
    public class CatsByBreedViewModel
    {
        public IEnumerable<CatDTO> Cats { get; set; } = default!;
        public IEnumerable<BreedDTO> Breeds { get; set; } = default!;

    }
}
