namespace GameHub.Services.DeviceService
{
    public interface IDeviceServices
    {
        Task<List<SelectListItem>> DeviceSelectedItemAsync();
    }
}
