using GameHub.Services.CatagoryService;
using GameHub.Services.DeviceService;

namespace GameHub.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICatogryServices _catogryServices;
        private readonly IDeviceServices _deviceServices;
        private readonly IGameService _gameService;

        public GamesController(
            ICatogryServices catogryServices,
            IDeviceServices deviceServices,
            IGameService gameService)
        {
            _catogryServices = catogryServices;
            _deviceServices = deviceServices;
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var games = await _gameService.GetGamesAsync();
            return View("Index", games);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateGameViewModel();
            await PopulateSelectListsAsync(viewModel);
            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameViewModel model)
        {
            await ValidateReferencesAsync(model.SelectedCatogryId, model.SelectedDeviceID);

            if (!await _gameService.CheckOnUnique(model.Name))
                ModelState.AddModelError(nameof(model.Name), "This game already exists.");

            if (!ModelState.IsValid)
            {
                await PopulateSelectListsAsync(model);
                return View("Create", model);
            }

            await _gameService.Create(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game is null)
                return NotFound();

            return View("Details", game);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game is null)
                return NotFound();

            var model = new EditGameFormViewModel
            {
                id = game.Id,
                Name = game.Name,
                SelectedCatogryId = game.CatogryId,
                SelectedDeviceID = game.GameDevices.Select(item => item.DeviceID).ToList(),
                Description = game.Description,
                CurrentCover = game.Cover
            };

            await PopulateSelectListsAsync(model);
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            await ValidateReferencesAsync(model.SelectedCatogryId, model.SelectedDeviceID);

            if (!await _gameService.CheckOnUnique(model.Name, model.id))
                ModelState.AddModelError(nameof(model.Name), "This game name is already in use.");

            if (!ModelState.IsValid)
            {
                await PopulateSelectListsAsync(model);
                return View("Edit", model);
            }

            var game = await _gameService.Edit(model);
            if (game is null)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _gameService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateSelectListsAsync(CreateGameViewModel model)
        {
            model.Devices = await _deviceServices.DeviceSelectedItemAsync();
            model.catogries = await _catogryServices.CatogrySelectedItemAsync();
        }

        private async Task PopulateSelectListsAsync(EditGameFormViewModel model)
        {
            model.Devices = await _deviceServices.DeviceSelectedItemAsync();
            model.catogries = await _catogryServices.CatogrySelectedItemAsync();
        }

        private async Task ValidateReferencesAsync(int categoryId, IEnumerable<int> deviceIds)
        {
            if (!await _gameService.CategoryExistsAsync(categoryId))
                ModelState.AddModelError(nameof(CreateGameViewModel.SelectedCatogryId), "Please select a valid category.");

            if (deviceIds is null || !await _gameService.DevicesExistAsync(deviceIds))
                ModelState.AddModelError(nameof(CreateGameViewModel.SelectedDeviceID), "Please select valid devices.");
        }
    }
}
