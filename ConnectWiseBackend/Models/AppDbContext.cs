﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ConnectWiseBackend.Models
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>  dbContext) : base(dbContext) 
        {
            

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Reminder> Reminder { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<ReminderLog> ReminderLog { get; set; }
        public DbSet<MasterEmail> MasterEmail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();

            modelBuilder.Entity<Company>()
                .HasOne<Subscription>(a => a.Subscription)
                .WithMany(a => a.Company)
                .HasForeignKey(k => k.SubscriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne<Branch>(a => a.Branch)
                .WithMany(a => a.Invoice)
                .HasForeignKey(k => k.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne<Invoice>(a => a.Invoice)
                .WithMany(a => a.Transaction)
                .HasForeignKey(k => k.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne<Branch>(a => a.Branch)
                .WithMany(a => a.User)
                .HasForeignKey(k => k.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne<Branch>(a => a.Branch)
                .WithMany(a => a.Invoice)
                .HasForeignKey(k => k.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
