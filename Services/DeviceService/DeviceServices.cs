namespace GameHub.Services.DeviceService
{
    public class DeviceServices : IDeviceServices
    {
        private readonly AppDbContext _context;
        public DeviceServices(AppDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> DeviceSelectedItem()
        {
            return _context.Devices.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
        }
    }
}
