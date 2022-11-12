using Microsoft.Extensions.Configuration;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5_job_schedule_with_email.JobScheduler
{
    public static class JobScheduler
    {
        public static void Jobs_Trigger_Schedule<Type>(this IServiceCollectionQuartzConfigurator serviceCollectionQuartzConfigurator, IConfiguration configuration) where Type : IJob
        {
            //get job key 
            string jobKeyName = typeof(Type).Name;

            //get time to schedule job
            long timeSchedule = configuration.GetValue<long>("jobScheduleTiming");

            //To Register Jobs
            JobKey jobKey = new JobKey(jobKeyName);
            serviceCollectionQuartzConfigurator.AddJob<Type>(option => option.WithIdentity(jobKey));

            //To Trigger Jobs
            serviceCollectionQuartzConfigurator.AddTrigger(option =>
            {
                option.ForJob(jobKey);
                option.WithIdentity(string.Concat(jobKey, "-", "trigger"));
                option.WithSimpleSchedule(schedule =>
                {
                    schedule.RepeatForever();
                    schedule.WithIntervalInSeconds((int)timeSchedule);
                });
            });
        }
    }
}
