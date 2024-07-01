using System;
﻿using Cooklee.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Data.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        //void SendEmail(Email email);
    }
}
