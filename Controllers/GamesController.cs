using GameHub.Services.CatagoryService;
using GameHub.Services.DeviceService;

namespace GameHub.Controllers
{
    public class GamesController : Controller
    {
        #region DI
        private readonly ICatogryServices _catogryServices;
        private readonly IDeviceServices _deviceServices;
        private readonly IGameService _gameService;
        public GamesController(ICatogryServices catogryServices, IDeviceServices deviceServices
            , IGameService gameService)
        {
            _catogryServices = catogryServices;
            _deviceServices = deviceServices;
            _gameService = gameService;
        }
        #endregion
        public IActionResult Index()
        {
            var x = _gameService.GetGames();
            return View("Index",x);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameViewModel viewModel = new()
            {
                Devices = _deviceServices.DeviceSelectedItem(),
                catogries=_catogryServices.CatogrySelectedItem()
            };  
            return View("Create",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameViewModel model)
        {
            if (!await _gameService.CheckOnUnique(model.Name))
                ModelState.AddModelError("Name", "This Game Already Exists");
            var num = model.SelectedCatogryId;
            if (ModelState.IsValid)
            {
                //save to db 
                //save image to server and save url 
                //redirect to index 
               await  _gameService.Create(model);
                 return RedirectToAction("Index","Home");
            }
            else
            {
                        
                model.Devices = _deviceServices.DeviceSelectedItem();
                model.catogries = _catogryServices.CatogrySelectedItem();
                return View("Create", model);
            }
            
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = _gameService.GetById(id);
            if(game is null)
                return NotFound();
            return View("Details",game);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game= _gameService.GetById(id);
            if(game is null)
                return NotFound();
            EditGameFormViewModel model = new()
            {
                id = id,
                Name = game.Name,
                SelectedCatogryId = game.CatogryId,
                SelectedDeviceID = game.GameDevices.Select(x => x.DeviceID).ToList(),
                Description = game.Description,
                Devices=_deviceServices.DeviceSelectedItem(),
                catogries=_catogryServices.CatogrySelectedItem(),
                CurrentCover=game.Cover,
            };
            return View("Edit", model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [CoverSizeValidation(5)]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {

            if (await _gameService.CheckOnUnique(model.Name,model.id)==false)
                ModelState.AddModelError("Name", "This Not Aavaliable ");
            if (ModelState.IsValid)
            { 
                
              var game=  await _gameService.Edit(model);
                if (game is null)
                    return BadRequest();
                return RedirectToAction("Index", "Home");
            }
            else
            {

                model.Devices = _deviceServices.DeviceSelectedItem();
                model.catogries = _catogryServices.CatogrySelectedItem();
                return View("Edit", model);
            }
        }
        [HttpGet]
        //we will do it with ajax 
        public IActionResult Delete(int id)
        {
            var isdeleted=_gameService.Delete(id);
            if(isdeleted)
                return RedirectToAction("Index", "Home");
            return BadRequest();
        } 
    }
}
