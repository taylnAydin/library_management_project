using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Core.Entities.Concrete;

namespace LibraryManagement.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RentedLog> RentedLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // niye degiskene atamadik
            modelBuilder.Entity<User>(entity => {
                entity.ToTable("users");
                entity.HasKey(u => u.Id); // İd dogrudan veremem patlar
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.Name).HasColumnName("name");
                entity.Property(u => u.Surname).HasColumnName("surname");
                entity.Property(u=>u.Email).HasColumnName("email");
                entity.Property(u=> u.Password).HasColumnName("password");
                entity.Property(u => u.Phone).HasColumnName("phone");
                entity.Property(u => u.BirthdayDate).HasColumnName("birthday_date");
                entity.Property(u => u.Role).HasColumnName("role").HasConversion<string>();
                entity.Property(u => u.Gender).HasColumnName("gender").HasConversion<string>(); //hasconversion ne
                entity.Property(u => u.Country).HasColumnName("country");
                entity.Property(u => u.IdentityCardNo).HasColumnName("identity_card_no");
                entity.Property(u => u.IsActive).HasColumnName("is_active");
                entity.Property(u => u.IsDeleted).HasColumnName("is_deleted");

            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");
                entity.HasKey(b => b.Id);
                entity.Property(b => b.Id).HasColumnName("id");
                entity.Property(b => b.Name).HasColumnName("name");
                entity.Property(b => b.Writer).HasColumnName("writer");
                entity.Property(b=> b.Category).HasColumnName("category");
                entity.Property(b => b.Stock).HasColumnName("stock");
                entity.Property(b => b.PublishDate).HasColumnName("publish_date");
                entity.Property(b => b.Publisher).HasColumnName("publisher");
                entity.Property(b => b.AddedDate).HasColumnName("added_date");
                entity.Property(b => b.Pages).HasColumnName("pages");
                entity.Property(b => b.IsDeleted).HasColumnName("is_deleted");
            });

            modelBuilder.Entity<RentedLog>(entity =>
            {
                entity.ToTable("rented_logs");
                entity.HasKey(r=>r.Id);
                entity.Property(r => r.Id).HasColumnName("id");
                entity.Property(r=>r.BookId).HasColumnName("book_id");
                entity.Property(r=>r.UserId).HasColumnName("user_id");
                entity.Property(r => r.StartDate).HasColumnName("start_date");
                entity.Property(r => r.DueDate).HasColumnName("due_date");
                entity.Property(r => r.ReturnDate).HasColumnName("return_date");
                entity.Property(r => r.Status).HasColumnName("status").HasConversion<string>();
                entity.Property(r => r.IsDeleted).HasColumnName("is_deleted");
            });


        }




    }
}
