using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Blazorvocabulary.Models;

#nullable disable

namespace Blazorvocabulary.Data
{
    public partial class TranslateDbContext : DbContext
    {
        public TranslateDbContext()
        {
        }

        public TranslateDbContext(DbContextOptions<TranslateDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TranslateList> TranslateLists { get; set; }

     /*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-SANN46V\\SQLEXPRESS;Initial Catalog=TranslateDb;Integrated Security=True");
            }
        }  */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<TranslateList>(entity =>
            {
               // entity.HasNoKey();

                entity.ToTable("TranslateList");

                entity.Property(e => e.EnglishTranslate)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.RowId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RowID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
