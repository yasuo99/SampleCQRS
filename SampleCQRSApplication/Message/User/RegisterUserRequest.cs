﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCQRSApplication.Message
{
    public class RegisterUserRequest
    {

        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
