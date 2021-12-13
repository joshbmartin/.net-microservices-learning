using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    //used to generate migrations in our sql db
    //probably not something used in prod, but something for testing
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            //create a service scope in order to derive a dbcontext
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                //Where do we get AppDbContext? If it was a nomral class it would get it from constructor DI,
                // but we can't do that. Create an instance or call this private method with an instance of AppDbContext
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }
        //do all the seeding of our data, and eventually run our migrations
        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                //push data in
                context.Platforms.AddRange(
                    new Platform() {Name="Dot Net", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="SQL Server Express", Publisher="Microsoft", Cost="Free"},
                    new Platform() {Name="Kubernetes", Publisher="Cloud Native Computing Foundation", Cost="Free"}
                );
                context.SaveChanges();
            }
            else 
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}