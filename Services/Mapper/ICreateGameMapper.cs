namespace GameHub.Services.Mapper
{
    public interface ICreateGameMapper
    {
        Task<Game> GameMapper(CreateGameViewModel viewModel);
        Task<string> CreateCoverName(IFormFile formFile);
    }
}
