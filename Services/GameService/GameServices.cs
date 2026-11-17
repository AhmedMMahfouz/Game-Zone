using GameHub.Services.Mapper;
using System.Diagnostics.Eventing.Reader;

namespace GameHub.Services.GameService
{
    public class GameServices : IGameService
    {
        private readonly AppDbContext _context;
        private readonly ICreateGameMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GameServices(AppDbContext context, ICreateGameMapper createGameMapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = createGameMapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Create(CreateGameViewModel createGameViewModel)
        {
            var Game = await _mapper.GameMapper(createGameViewModel);
            _context.Games.Add(Game);
            _context.SaveChanges();
        }
        public IEnumerable<Game> GetGames()
        {
            return _context.Games.AsNoTracking().Include(x => x.Catogry)
                 .Include(x => x.GameDevices).ThenInclude(g => g.Device).ToList();
        }
        public async Task<bool> CheckOnUnique(string GameName, int? GameId = null)
        {
            return !await _context.Games.AnyAsync(x => x.Name == GameName && x.Id != GameId);

        }
        public Game? GetById(int id)
        {
            return _context.Games.AsNoTracking().Include(x => x.Catogry).Include(x => x.GameDevices).ThenInclude(x => x.Device).SingleOrDefault(x => x.Id == id);
        }
        public async Task<Game?> Edit(EditGameFormViewModel editGameFormViewModel)
        {
            var game = _context.Games.Include(g => g.GameDevices)
                .SingleOrDefault(g => g.Id == editGameFormViewModel.id);
            if (game == null)
                return null;
            var hasnewcover = editGameFormViewModel.CoverUrl is not null;
            var oldCover = game.Cover;
            game.Name = editGameFormViewModel.Name;
            game.Description = editGameFormViewModel.Description;
            game.CatogryId = editGameFormViewModel.SelectedCatogryId;
            game.GameDevices = editGameFormViewModel.SelectedDeviceID.Select(x => new GameDevice
            {
                DeviceID = x
            }).ToList();
            if (hasnewcover)
            {
                game.Cover = await _mapper.CreateCoverName(editGameFormViewModel.CoverUrl!);
            }

            var effectedrows = _context.SaveChanges();
            if (effectedrows > 0)
            {
                if (hasnewcover)
                {
                    var cover = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "Image", "Games", oldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                return game;
            }


        }

        public bool Delete(int id)
        {
            var isdeleted = false;
            var game = _context.Games.Find(id);
            if (game is null)
                return isdeleted;
            _context.Games.Remove(game);
            var effectedrows = _context.SaveChanges();
            if (effectedrows > 0)
            {
                isdeleted = true;
                var cover = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "Image", "Games", game.Cover);
                File.Delete(cover);
            }

            return isdeleted;
        }
    }
}
