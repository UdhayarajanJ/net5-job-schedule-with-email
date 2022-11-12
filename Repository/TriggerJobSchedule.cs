using net5_job_schedule_with_email.Contracts;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5_job_schedule_with_email.Repository
{
    public class TriggerJobSchedule : IJob
    {
        private IEmailContracts _emailContracts;
        public TriggerJobSchedule(IEmailContracts emailContracts)
        {
            _emailContracts = emailContracts;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _emailContracts.SendMail("jobscheduler.udhaya.mailtrap@gmail.com", "JobScheduler");
        }
    }
}
