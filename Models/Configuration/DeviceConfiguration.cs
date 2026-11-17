using GameHub.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameHub.Models.Configuration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(150);
            builder.Property(e => e.Icon).HasMaxLength(250);
            builder.ToTable("Devices");
            builder.HasData(new Device[]
            {
                new Device{Id=1,Name="Playstation",Icon="bi bi-playstation"},
                new Device{Id=2,Name="xbox",Icon="bi bi-xbox"},
                new Device{Id=3,Name="Nintendo Switch",Icon="bi bi-nintendo-switch"},
                new Device{Id=4,Name="PC",Icon="bi bi-PC-display"}
            });
        }
    }
}
