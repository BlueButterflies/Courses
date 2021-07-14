using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Courses.Models.Entity;

#nullable disable

namespace Courses.Models.Services.Infrastructure
{
    public partial class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Version", "12345");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");//SUPERFLUOUS IF THE TABLE IS NAMED AS THE PROPERTY THAT EXPOSES DBSET
               entity.HasKey(course => course.Id);//SUPERFLUOUS IF THE TABLE IS CALLED LIKE THE PROPERTY ID OR CoursesId


                #region Mapping owned types
                entity.OwnsOne(course => course.FullPrice, builder => {
                    builder.Property(money => money.Currency)
                           .HasConversion<string>();
                    builder.Property(money => money.Amount); //Questo indica al meccanismo delle migration che la colonna della tabella dovrà essere creata di tipo numerico
                });

                entity.OwnsOne(course => course.DiscountPrice, builder => {
                    builder.Property(money => money.Currency)
                           .HasConversion<string>()
                           .HasColumnName("DiscountPrice_Currency"); //Superfluo perché le nostre colonne seguono già la convenzione di nomi
                    builder.Property(money => money.Amount)
                           .HasColumnName("DiscountPrice_Amount");//Superfluo perché le nostre colonne seguono già la convenzione di nomi
                });
                #endregion

                #region Mapping generate automatic from tool reverse engineering
                //entity.Property(e => e.Author)
                //     .IsRequired()
                //     .HasColumnType("varchar(250)");

                //entity.Property(e => e.Descriptions)
                //    .IsRequired()
                //    .HasColumnType("text");

                //entity.Property(e => e.DiscountPrice)
                //    .HasColumnType("decimal(18, 0)")
                //    .HasColumnName("DiscountPrice_Amount");

                //entity.Property(e => e.DiscountPrice)
                //    .IsRequired()
                //    .HasColumnType("varchar(50)")
                //    .HasColumnName("DiscountPrice_Currency")
                //    .HasDefaultValueSql("('EUR')");

                //entity.Property(e => e.Email)
                //    .IsRequired()
                //    .HasColumnType("varchar(150)");

                //entity.Property(e => e.FullPrice)
                //    .HasColumnType("decimal(18, 0)")
                //    .HasColumnName("FullPrice_Amount");

                //entity.Property(e => e.FullPrice)
                //    .IsRequired()
                //    .HasColumnType("varchar(50)")
                //    .HasColumnName("FullPrice_Currency")
                //    .HasDefaultValueSql("('EUR')");

                //entity.Property(e => e.ImagePath).HasColumnType("varchar(150)");

                //entity.Property(e => e.Title)
                //    .IsRequired()
                //    .HasColumnType("varchar(250)");
                #endregion

                #region Mapping relations

                entity.HasMany(course => course.Lessons)
                      .WithOne(lesson => lesson.Course)
                      .HasForeignKey(lesson => lesson.CourseId);//SUPERFLUOUS IF THE TABLE IS CALLED LIKE THE PROPERTY CoursesId
                #endregion

            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                //entity.HasOne(lesson => lesson.Course)
                //      .WithMany(course => course.Lessons)
                //      .HasForeignKey(lesson => lesson.CourseId);
                #region Mapping generate automatic from tool reverse engineering
                /* entity.Property(e => e.Id).ValueGeneratedNever();

                 entity.Property(e => e.Descriptions).HasColumnType("text");

                 entity.Property(e => e.Duration)
                     .IsRequired()
                     .HasColumnType("text")
                     .HasColumnName("Duration ")
                     .HasDefaultValueSql("('00:00:00')");

                 entity.Property(e => e.Title)
                     .IsRequired()
                     .HasColumnType("text");

                 entity.HasOne(d => d.IdNavigation)
                     .WithOne(p => p.Lesson)
                     .HasForeignKey<Lesson>(d => d.Id)
                     .HasConstraintName("FK_Lessons_Courses");*/
                #endregion
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
