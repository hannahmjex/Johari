using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Adjective> Adjectives { get; set; }
        public DbSet<ClientResponses> ClientResponses { get; set; }
        public DbSet<Friend> Friend { get; set; }
        public DbSet<FriendResponses> FriendResponses { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
