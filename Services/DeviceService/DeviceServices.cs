namespace GameHub.Services.DeviceService
{
    public class DeviceServices : IDeviceServices
    {
        private readonly AppDbContext _context;

        public DeviceServices(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<SelectListItem>> DeviceSelectedItemAsync()
        {
            return _context.Devices
                .AsNoTracking()
                .OrderBy(device => device.Name)
                .Select(device => new SelectListItem
                {
                    Text = device.Name,
                    Value = device.Id.ToString()
                })
                .ToListAsync();
        }
    }
}
