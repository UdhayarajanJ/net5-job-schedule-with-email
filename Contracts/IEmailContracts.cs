using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5_job_schedule_with_email.Contracts
{
    public interface IEmailContracts
    {
        public Task SendMail(string emailAddress, string subject);
    }
}
