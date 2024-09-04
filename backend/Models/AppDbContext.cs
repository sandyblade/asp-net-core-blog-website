/**
 * This file is part of the Sandy Andryanto Blog Application.
 *
 * @author     Sandy Andryanto <sandy.andryanto.blade@gmail.com>
 * @copyright  2024
 *
 * For the full copyright and license information,
 * please view the LICENSE.md file that was distributed
 * with this source code.
 */

using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Viewer> Viewer { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasOne(c => c.User).WithMany(n => n.Activities).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Article>().HasOne(c => c.User).WithMany(n => n.Articles).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(n => n.Comments).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(c => c.Article).WithMany(n => n.Comments).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().HasOne(c => c.Parent).WithMany(n => n.Children).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Notification>().HasOne(c => c.User).WithMany(n => n.Notifications).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Viewer>().HasOne(c => c.Article).WithMany(n => n.Viewers).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Viewer>().HasOne(c => c.User).WithMany(n => n.Viewers).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
