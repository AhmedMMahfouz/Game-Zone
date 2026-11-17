namespace GameHub.Models.Entity
{
    public class Game:EntityType
    {
        public string Description { get; set; }
        public string Cover {  get; set; }
        public Catogry Catogry { get; set; } = null!;
        public int CatogryId {  get; set; }
        public ICollection<GameDevice> GameDevices { get; set; }= null!;

    }
}
