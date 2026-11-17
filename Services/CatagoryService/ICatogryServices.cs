namespace GameHub.Services.CatagoryService
{
    public interface ICatogryServices
    {
        Task<List<SelectListItem>> CatogrySelectedItemAsync();
    }
}
