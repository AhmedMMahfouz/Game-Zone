using GameHub.Services.CatagoryService;

namespace GameHub.Services
{
    public class CatogryServices : ICatogryServices
    {
        private readonly AppDbContext _context;

        public CatogryServices(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<SelectListItem>> CatogrySelectedItemAsync()
        {
            return _context.Catogries
                .AsNoTracking()
                .OrderBy(category => category.Name)
                .Select(category => new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Name
                })
                .ToListAsync();
        }
    }
}
