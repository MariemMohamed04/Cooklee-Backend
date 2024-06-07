﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooklee.Core.DTOs
{
    public class ClientProfileDto
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string? ImgURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
