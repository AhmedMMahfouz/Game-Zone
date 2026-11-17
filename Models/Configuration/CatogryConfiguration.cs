
namespace GameHub.Models.Configuration
{
    public class CatogryConfiguration : IEntityTypeConfiguration<Catogry>
    {
        public void Configure(EntityTypeBuilder<Catogry> builder)
        {
            builder.Property(x=>x.Name).HasMaxLength(256);
            builder.ToTable("Catogries");
            builder.HasData(new Catogry[]
            {
                new Catogry{Id=1,Name="Action"},
                new Catogry{Id=2,Name="Adventure"},
                new Catogry{Id=3,Name="Strategy"},
                new Catogry{Id=4,Name="Horror"},
                new Catogry{Id=5,Name="Fighting"},
                new Catogry{Id=6,Name="Sports"},
            });
        }
    }
}
