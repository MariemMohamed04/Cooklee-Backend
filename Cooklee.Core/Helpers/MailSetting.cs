using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.Helpers
{
    public class MailSetting
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }        
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
