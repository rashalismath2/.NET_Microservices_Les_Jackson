using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Data
{
	public static class Seeds
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Platform>()
                .HasData(
                    new Platform
                    {
                        Id =1,
                        Name = "Dot net",
                        Publisher = "Microsoft",
                        Cost = "Free"
                    }
                );
        }
	
	}
}
