using Cooklee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.Helpers
{
    public interface IEmailSetting
    {
        void SendEmailAsync(Email email);
    }
}
