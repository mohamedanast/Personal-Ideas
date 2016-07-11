namespace Ideas.DataAccess.Model
{
    using System;
    using System.Data.Entity;
    using Ideas.DataAccess.Entities;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class IdeasContext : DbContext
    {
        public IdeasContext()
            : base("name=IdeasContext")
        {
        }

        public virtual DbSet<IdeaAttachment> IdeaAttachments { get; set; }
        public virtual DbSet<Idea> Ideas { get; set; }
        public virtual DbSet<IdeaTag> IdeaTags { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Idea>()
                .Property(e => e.Created)
                .HasPrecision(2);

            modelBuilder.Entity<Idea>()
                .Property(e => e.Modified)
                .HasPrecision(2);

            modelBuilder.Entity<Idea>()
                .HasMany(e => e.IdeaAttachments)
                .WithRequired(e => e.Idea)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Idea>()
                .HasMany(e => e.IdeaTags)
                .WithRequired(e => e.Idea)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Created)
                .HasPrecision(2);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.IdeaTags)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);
        }
    }
}
