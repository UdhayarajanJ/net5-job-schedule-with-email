using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using net5_job_schedule_with_email.JobScheduler;
using net5_job_schedule_with_email.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5_job_schedule_with_email
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })


                //Job Schedule Configurations
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddQuartz(option =>
                    {
                        option.UseMicrosoftDependencyInjectionJobFactory();
                        option.Jobs_Trigger_Schedule<TriggerJobSchedule>(hostContext.Configuration);
                    });

                    services.AddQuartzHostedService(option => option.WaitForJobsToComplete = true);
                });
    }
}
