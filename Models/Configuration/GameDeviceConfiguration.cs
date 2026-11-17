
namespace GameHub.Models.Configuration
{
    public class GameDeviceConfiguration : IEntityTypeConfiguration<GameDevice>
    {
        public void Configure(EntityTypeBuilder<GameDevice> builder)
        {
            builder.HasKey(x => new
            {
                x.DeviceID,x.GameID
            });
            builder.HasOne(x => x.Game)
                .WithMany(g => g.GameDevices)
                .HasForeignKey(x => x.GameID);

            builder.HasOne(x => x.Device)
                .WithMany(d => d.GameDevices)
                .HasForeignKey(x => x.DeviceID);
            builder.ToTable($"{nameof(GameDevice)}s");
        }
    }
}
