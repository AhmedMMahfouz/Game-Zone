namespace GameHub.Services.Mapper
{
    public class CreateGameMapper : ICreateGameMapper
    {
        private readonly IWebHostEnvironment _environment;

        public CreateGameMapper(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<Game> GameMapper(CreateGameViewModel viewModel)
        {
            var game = new Game
            {
                Name = viewModel.Name.Trim(),
                Description = viewModel.Description.Trim(),
                GameDevices = viewModel.SelectedDeviceID.Distinct()
                    .Select(id => new GameDevice { DeviceID = id })
                    .ToList(),
                CatogryId = viewModel.SelectedCatogryId
            };

            game.Cover = await SaveCoverAsync(viewModel.CoverUrl);
            return game;
        }

        public Task<string> CreateCoverName(IFormFile formFile)
        {
            return SaveCoverAsync(formFile);
        }

        private async Task<string> SaveCoverAsync(IFormFile formFile)
        {
            var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(formFile.FileName).ToLowerInvariant()}";
            var folderPath = Path.Combine(_environment.WebRootPath, "assets", "Image", "Games");
            Directory.CreateDirectory(folderPath);

            var fileLocation = Path.Combine(folderPath, fileName);
            await using var stream = new FileStream(
                fileLocation, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, useAsync: true);

            await formFile.CopyToAsync(stream);
            return fileName;
        }
    }
}
