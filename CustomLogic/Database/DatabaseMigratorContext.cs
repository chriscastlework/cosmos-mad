namespace CustomLogic.Database
{
    using Microsoft.EntityFrameworkCore;
    using RecruitMe.Web.Database;

    public partial class DatabaseMigratorContext : DbContext
    {
        public DatabaseMigratorContext()
        {
        }

        public DatabaseMigratorContext(DbContextOptions<DatabaseMigratorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CreatedFiles> CreatedFiles { get; set; }
        public virtual DbSet<DocumentRecords> DocumentRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreatedFiles>(entity =>
            {
                entity.Property(e => e.CsFiles).IsRequired();

                entity.Property(e => e.TsFile).IsRequired();
            });

            modelBuilder.Entity<DocumentRecords>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Partition })
                    .HasName("PK_DatabaseClassInformation");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_DatabaseClassInformation");

                entity.Property(e => e.Id).HasMaxLength(100);

                entity.Property(e => e.Partition).HasMaxLength(100);

                entity.Property(e => e.EntityType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.JsonDocument).IsRequired();

                entity.Property(e => e.Ts).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedFiles)
                    .WithMany(p => p.DocumentRecords)
                    .HasForeignKey(d => d.CreatedFilesId)
                    .HasConstraintName("FK_DocumentRecords_CreatedFiles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
