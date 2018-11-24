using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserDbWebApi.Data;

namespace UserDbWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {        
            Console.Title = "UserDbWebApi";

            var seed = args.Any(x => x == "/seed");
            if (seed) args = args.Except(new[] { "/seed" }).ToArray();

            var host = BuildWebHost(args);

            //seed = true; //DEBUG
            if (seed)
            {
                Console.WriteLine(">>>>>> Start Seeding data");
                SeedData.EnsureSeedData(host.Services);
                Console.WriteLine("<<<<<< Seed data Sucsesful");
                Console.ReadLine();
                return;
            }

            host.Run();
        }


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
