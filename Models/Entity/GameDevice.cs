namespace GameHub.Models.Entity
{
    public class GameDevice
    {
        public int GameID {  get; set; }
        public Game Game { get; set; } = null!;
        public int DeviceID { get; set; }
        public Device Device { get; set; } = null!;
    }
}
