namespace GameHub.Models.Entity
{
    public class Catogry:EntityType
    {
        public ICollection<Game> Games { get; set; } = null!;
    }
}
