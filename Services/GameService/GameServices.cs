using GameHub.Services.Mapper;

namespace GameHub.Services.GameService
{
    public class GameServices : IGameService
    {
        private readonly AppDbContext _context;
        private readonly ICreateGameMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<GameServices> _logger;

        public GameServices(
            AppDbContext context,
            ICreateGameMapper createGameMapper,
            IWebHostEnvironment webHostEnvironment,
            ILogger<GameServices> logger)
        {
            _context = context;
            _mapper = createGameMapper;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task Create(CreateGameViewModel createGameViewModel)
        {
            var game = await _mapper.GameMapper(createGameViewModel);

            try
            {
                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();
            }
            catch
            {
                TryDeleteCover(game.Cover);
                throw;
            }
        }

        public async Task<IReadOnlyList<Game>> GetGamesAsync()
        {
            return await _context.Games
                .AsNoTracking()
                .Include(game => game.Catogry)
                .Include(game => game.GameDevices)
                    .ThenInclude(gameDevice => gameDevice.Device)
                .OrderBy(game => game.Name)
                .ToListAsync();
        }

        public async Task<bool> CheckOnUnique(string gameName, int? gameId = null)
        {
            return !await _context.Games
                .AnyAsync(game => game.Name == gameName && game.Id != gameId);
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games
                .AsNoTracking()
                .Include(game => game.Catogry)
                .Include(game => game.GameDevices)
                    .ThenInclude(gameDevice => gameDevice.Device)
                .SingleOrDefaultAsync(game => game.Id == id);
        }

        public async Task<Game?> Edit(EditGameFormViewModel model)
        {
            var game = await _context.Games
                .Include(current => current.GameDevices)
                .SingleOrDefaultAsync(current => current.Id == model.id);

            if (game is null)
                return null;

            var oldCover = game.Cover;
            string? newCover = null;

            game.Name = model.Name.Trim();
            game.Description = model.Description.Trim();
            game.CatogryId = model.SelectedCatogryId;

            var selectedDeviceIds = model.SelectedDeviceID.Distinct().ToList();
            game.GameDevices.Clear();
            foreach (var deviceId in selectedDeviceIds)
            {
                game.GameDevices.Add(new GameDevice { DeviceID = deviceId });
            }

            if (model.CoverUrl is not null)
            {
                newCover = await _mapper.CreateCoverName(model.CoverUrl);
                game.Cover = newCover;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (newCover is not null)
                    TryDeleteCover(newCover);

                throw;
            }

            if (newCover is not null)
                TryDeleteCover(oldCover);

            return game;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game is null)
                return false;

            _context.Games.Remove(game);
            var affectedRows = await _context.SaveChangesAsync();

            if (affectedRows <= 0)
                return false;

            TryDeleteCover(game.Cover);
            return true;
        }

        public Task<bool> CategoryExistsAsync(int categoryId)
        {
            return _context.Catogries.AnyAsync(category => category.Id == categoryId);
        }

        public async Task<bool> DevicesExistAsync(IEnumerable<int> deviceIds)
        {
            var ids = deviceIds.Distinct().ToList();
            if (ids.Count == 0)
                return false;

            return await _context.Devices.CountAsync(device => ids.Contains(device.Id)) == ids.Count;
        }

        private void TryDeleteCover(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var coverPath = Path.Combine(
                _webHostEnvironment.WebRootPath,
                "assets", "Image", "Games", fileName);

            try
            {
                if (File.Exists(coverPath))
                    File.Delete(coverPath);
            }
            catch (IOException exception)
            {
                _logger.LogWarning(exception, "Could not delete game cover {CoverName}", fileName);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogWarning(exception, "No permission to delete game cover {CoverName}", fileName);
            }
        }
    }
}
