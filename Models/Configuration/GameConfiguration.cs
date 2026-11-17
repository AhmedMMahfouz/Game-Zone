using GameHub.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameHub.Models.Configuration
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).HasMaxLength(150);
            builder.Property(e => e.Description).HasMaxLength(2500);
            builder.Property(e => e.Cover).HasMaxLength(500);
            builder.HasOne(g => g.Catogry)
                .WithMany(c => c.Games)
                .HasForeignKey(g => g.CatogryId);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.ToTable("Games");
        }
    }
}
