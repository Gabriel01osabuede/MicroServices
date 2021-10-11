using CommandsService.Models;
using CommandsService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Data
{
    public static class PrebDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

                var platforms = grpcClient.ReturnAllPlatforms();

                SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms,isProd,serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }


        private static void SeedData(ICommandRepo repo, IEnumerable<platform> platforms,bool isProd,AppDbContext context)
        {

            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"--> Could not run migrations {ex.Message}");
                }
            }
            if(!context.Commands.Any())
            {
                Console.WriteLine("--> Seeding new Platforms....");
                foreach (var plat in platforms)
                {
                    if (!repo.ExternalPlatformExist(plat.ExternalID))
                    {
                        repo.CreatePlatform(plat);
                    }
                    repo.SaveChanges();
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data.");
            }
           

            
            
        }
    }
}
