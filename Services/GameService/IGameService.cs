namespace GameHub.Services.Interface
{
    public interface IGameService
    {
        Task Create(CreateGameViewModel createGameViewModel);
        Task<IReadOnlyList<Game>> GetGamesAsync();
        Task<bool> CheckOnUnique(string gameName, int? gameId = null);
        Task<Game?> GetByIdAsync(int id);
        Task<Game?> Edit(EditGameFormViewModel editGameFormViewModel);
        Task<bool> DeleteAsync(int id);
        Task<bool> CategoryExistsAsync(int categoryId);
        Task<bool> DevicesExistAsync(IEnumerable<int> deviceIds);
    }
}
