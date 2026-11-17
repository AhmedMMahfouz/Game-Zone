namespace GameHub.Services.Interface
{
    public interface IGameService
    {
        Task Create(CreateGameViewModel createGameViewModel);
        IEnumerable<Game> GetGames();
        Task<bool> CheckOnUnique(string GameName, int? GameId = null);
        Game? GetById(int id);
        Task<Game>? Edit(EditGameFormViewModel editGameFormViewModel);
        bool Delete(int id);
    }
}
