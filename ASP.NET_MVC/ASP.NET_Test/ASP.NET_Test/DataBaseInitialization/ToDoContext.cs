using ASP.NET_Test.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ASP.NET_Test.DataBaseInitialization
{
    public class ToDoContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey<int>(a => a.AccountId)
                .Property(a => a.AccountId)
                .HasColumnOrder(1)
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Account>()
                .Property(a => a.Email)
                .HasColumnName("Email")
                .HasColumnOrder(2)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(a => a.Password)
                .HasColumnName("Password")
                .HasColumnOrder(3)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();


            modelBuilder.Entity<Category>().HasKey<int>(c => c.CategoryId)
                .Property(c => c.CategoryId)
                .HasColumnOrder(1)
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryName)
                .HasColumnName("CategoryName")
                .HasColumnOrder(2)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_AccountCategory", 2) { IsUnique = true }));

            modelBuilder.Entity<Category>()
                .Property(a => a.AccountRefId)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                new IndexAttribute("IX_AccountCategory", 1) { IsUnique = true }));

            modelBuilder.Entity<Task>().HasKey<int>(t => t.TaskId)
                .Property(t => t.TaskId)
                .HasColumnOrder(1)
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Task>()
                .Property(t => t.TaskName)
                .HasColumnName("TaskName")
                .HasColumnOrder(2)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<TaskItem>().HasKey<int>(ti => ti.TaskItemId)
                .Property(ti => ti.TaskItemId)
                .HasColumnOrder(1)
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<TaskItem>()
                .Property(ti => ti.TaskItemName)
                .HasColumnName("TaskItemName")
                .HasColumnOrder(2)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<TaskItem>()
                .Property(ti => ti.Status)
                .HasColumnName("Status")
                .HasColumnOrder(3)
                .HasColumnType("bit")
                .IsRequired();

            modelBuilder.Entity<Account>()
                .HasMany(c => c.Categories)
                .WithOptional(a => a.Account)
                .HasForeignKey(c => c.AccountRefId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Task>()
                 .HasMany<TaskItem>(ti => ti.TaskItems)
                 .WithOptional(t => t.Task)
                 .HasForeignKey(t => t.TaskRefId)
                 .WillCascadeOnDelete(true);

            modelBuilder.Entity<Category>()
                 .HasMany<Task>(t => t.Tasks)
                 .WithMany(c => c.Categories)
                 .Map(ct =>
                 {
                     ct.MapLeftKey("TaskId");
                     ct.MapRightKey("CategoryId");
                     ct.ToTable("TaskCategory");
                 });

        }
    }
}