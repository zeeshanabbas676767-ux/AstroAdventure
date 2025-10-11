using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AstroAdventure.Models
{
    public partial class AstroSpace : DbContext
    {
        public AstroSpace()
            : base("name=AstroSpace")
        {
        }

        public virtual DbSet<tblArticle> tblArticles { get; set; }
        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblComment> tblComments { get; set; }
        public virtual DbSet<tblImage> tblImages { get; set; }
        public virtual DbSet<tblMission> tblMissions { get; set; }
        public virtual DbSet<tblPlanet> tblPlanets { get; set; }
        public virtual DbSet<tblStar> tblStars { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCategory>()
                .HasMany(e => e.tblArticles)
                .WithRequired(e => e.tblCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblPlanet>()
                .HasMany(e => e.tblMissions)
                .WithOptional(e => e.tblPlanet)
                .HasForeignKey(e => e.RelatedPlanetID);

            modelBuilder.Entity<tblUser>()
                .HasMany(e => e.tblArticles)
                .WithRequired(e => e.tblUser)
                .HasForeignKey(e => e.AuthorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblUser>()
                .HasMany(e => e.tblComments)
                .WithRequired(e => e.tblUser)
                .WillCascadeOnDelete(false);
        }
    }
}
