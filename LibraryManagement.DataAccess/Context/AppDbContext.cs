using Microsoft.EntityFrameworkCore;
using LibraryManagement.Core.Entities.Concrete;

namespace LibraryManagement.DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RentedLog> RentedLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // USERS
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", table =>
                {
                    table.HasCheckConstraint(
                        "users_gender_check",
                        "gender IN ('FEMALE', 'MALE')");

                    table.HasCheckConstraint(
                        "users_role_check",
                        "role IN ('LIBRARIAN', 'MEMBER')");
                });

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(u => u.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(u => u.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .HasMaxLength(150);

                entity.Property(u => u.Password)
                    .HasColumnName("password")
                    .HasMaxLength(255);

                entity.Property(u => u.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.Property(u => u.BirthdayDate)
                    .HasColumnName("birthday_date");

                entity.Property(u => u.Role)
                    .HasColumnName("role")
                    .HasMaxLength(20)
                    .HasConversion<string>();

                entity.Property(u => u.Gender)
                    .HasColumnName("gender")
                    .HasMaxLength(10)
                    .HasConversion<string>();

                entity.Property(u => u.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(u => u.IdentityCardNo)
                    .HasColumnName("identity_card_no")
                    .HasMaxLength(20);


                entity.Property(u => u.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValue(false);

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.HasIndex(u => u.IdentityCardNo)
                    .IsUnique();
            });


            // BOOKS
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books", table =>
                {
                    table.HasCheckConstraint(
                        "books_pages_check",
                        "pages > 0");

                    table.HasCheckConstraint(
                        "books_stock_check",
                        "stock >= 0");
                });

                entity.HasKey(b => b.Id);

                entity.Property(b => b.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(b => b.Title)
                    .HasColumnName("title")
                    .HasMaxLength(150);

                entity.Property(b => b.Writer)
                    .HasColumnName("writer")
                    .HasMaxLength(100);

                entity.Property(b => b.Category)
                    .HasColumnName("category")
                    .HasMaxLength(50);

                entity.Property(b => b.Stock)
                    .HasColumnName("stock")
                    .HasDefaultValue(0);

                entity.Property(b => b.PublishDate)
                    .HasColumnName("publish_date");

                entity.Property(b => b.Publisher)
                    .HasColumnName("publisher")
                    .HasMaxLength(100);

                entity.Property(b => b.AddedDate)
                    .HasColumnName("added_date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(b => b.Pages)
                    .HasColumnName("pages");

                entity.Property(b => b.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValue(false);
            });


            // RENTED LOGS
            modelBuilder.Entity<RentedLog>(entity =>
            {
                entity.ToTable("rented_logs", table =>
                {
                    table.HasCheckConstraint(
                        "rented_logs_status_check",
                        "status IN ('RENTED', 'RETURNED')");
                });

                entity.HasKey(r => r.Id);

                entity.Property(r => r.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(r => r.BookId)
                    .HasColumnName("book_id");

                entity.Property(r => r.UserId)
                    .HasColumnName("user_id");

                entity.Property(r => r.StartDate)
                    .HasColumnName("start_date")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(r => r.DueDate)
                    .HasColumnName("due_date");

                entity.Property(r => r.ReturnDate)
                    .HasColumnName("return_date");

                entity.Property(r => r.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .HasConversion<string>()
                    .HasDefaultValueSql("'RENTED'");

                entity.Property(r => r.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasDefaultValue(false);

                entity.HasOne(r => r.Book)
                    .WithMany()
                    .HasForeignKey(r => r.BookId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(r => r.User)
                    .WithMany(u => u.RentedLogs)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}