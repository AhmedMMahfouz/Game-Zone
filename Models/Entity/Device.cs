namespace GameHub.Models.Entity
{
    public class Device:EntityType
    {
        public string Icon {  get; set; }=string.Empty;
        public ICollection<GameDevice>GameDevices { get; set; }= null!;

    }
}
