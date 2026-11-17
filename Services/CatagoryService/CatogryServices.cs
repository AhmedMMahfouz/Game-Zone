
using GameHub.Services.CatagoryService;

namespace GameHub.Services
{
    public class CatogryServices:ICatogryServices
    {
        private readonly AppDbContext _context;

        public CatogryServices(AppDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> CatogrySelectedItem()
        {

            return _context.Catogries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }
    }
}
