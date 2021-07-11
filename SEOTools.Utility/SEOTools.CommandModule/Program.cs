using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SEOTools.Utility;
using System;

namespace SEOTools.CommandModule
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Logging Hosting Environment Started");

            Console.WriteLine("Welcome to the SEO Tools Command Module");
            Console.WriteLine("The package is created by Rohan Kondvilkar and is open to free use " +
                "and distribution as long as proper attribution is provided to the orginal source code creators");
            CommandMenu();
        }

        private static void CommandMenu()
        {
            Console.WriteLine("Please select the below mentioned options");
            Console.WriteLine("1. Site Health - Linking Strategy Check");
            string commandKey = Console.ReadLine();
            switch (commandKey)
            {
                case "1":
                    Console.WriteLine("Please provide the URL of your website");
                    string url = Console.ReadLine();
                    url = "https://cardiothinklab.com";
                    URLValidator uRLValidator = new URLValidator();
                    uRLValidator.PerformURLValidation(url);
                    break;

                default:
                    Console.WriteLine("Error: Please provide the correct command");
                    CommandMenu();
                    break;
            }
        }
    }
}
