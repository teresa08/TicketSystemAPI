﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request.User
{
    public class httpAuthenticateUserRequest
    {

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
