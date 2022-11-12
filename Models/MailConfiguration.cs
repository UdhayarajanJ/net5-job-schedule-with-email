using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net5_job_schedule_with_email.Models
{
    public class MailConfiguration
    {
        public string name { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
    }
}
