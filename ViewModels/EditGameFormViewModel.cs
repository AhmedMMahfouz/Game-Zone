namespace GameHub.ViewModels
{
    public class EditGameFormViewModel
    {
        public int id {  get; set; }
        [MaxLength(150)]
        public string Name { get; set; } = default!;
        [MaxLength(2500, ErrorMessage = "you need to shortage the discription")]
        public string Description { get; set; } = default!;
        [Display(Name = "Cover")]
        [CoverTypeValidation(".jpg,.jpeg")]
        [CoverSizeValidation(2)]
        public IFormFile?  CoverUrl { get; set; } 
        [Display(Name = "Game Catogry")]
        public int SelectedCatogryId { get; set; }
        public List<SelectListItem>? catogries { get; set; } = default!;
        [Display(Name = "Devices")]
        [Required]
        public List<int> SelectedDeviceID { get; set; }
        public List<SelectListItem>? Devices { get; set; } = default!;
        public string? CurrentCover {  get; set; }
    }
}
