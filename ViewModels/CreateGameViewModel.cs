namespace GameHub.ViewModels
{
    public class CreateGameViewModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(2500, ErrorMessage = "Please shorten the description.")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Cover")]
        [CoverTypeValidation(".jpg,.jpeg,.png,.webp")]
        [CoverSizeValidation(3)]
        public IFormFile CoverUrl { get; set; } = default!;

        [Display(Name = "Game Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int SelectedCatogryId { get; set; }

        public List<SelectListItem> catogries { get; set; } = new();

        [Display(Name = "Devices")]
        [Required]
        [MinLength(1, ErrorMessage = "Select at least one device.")]
        public List<int> SelectedDeviceID { get; set; } = new();

        public List<SelectListItem> Devices { get; set; } = new();
    }
}
