namespace GameHub.Services.Mapper
{
    public class CreateGameMapper : ICreateGameMapper
    {
        private readonly IWebHostEnvironment _environment;

        public CreateGameMapper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
         
        public async Task<Game>GameMapper(CreateGameViewModel viewModel)
        {
            var Game = new Game()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                GameDevices = viewModel.SelectedDeviceID.Select(x=>new GameDevice { DeviceID =x}).ToList(),
                CatogryId=viewModel.SelectedCatogryId
            };
            var FileName=$"{Guid.NewGuid().ToString()}{Path.GetExtension(viewModel.CoverUrl.FileName)}";
            var FolderPath = Path.Combine(_environment.WebRootPath, "assets", "Image", "Games");
            Directory.CreateDirectory(FolderPath);
            var FileLocation = Path.Combine(FolderPath, FileName);
            using(var stream = new FileStream(FileLocation,FileMode.Create,FileAccess.Write))
                await viewModel.CoverUrl.CopyToAsync(stream);
            Game.Cover = FileName;
            return Game;
        }
        public async  Task<string> CreateCoverName(IFormFile formFile)
        {
            var FileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(formFile.FileName)}";
            var FolderPath = Path.Combine(_environment.WebRootPath, "assets", "Image", "Games");
            Directory.CreateDirectory(FolderPath);
            var FileLocation = Path.Combine(FolderPath, FileName);
            using (var stream = new FileStream(FileLocation, FileMode.Create, FileAccess.Write))
                await formFile.CopyToAsync(stream);
            return FileName;
        }
    }
}
