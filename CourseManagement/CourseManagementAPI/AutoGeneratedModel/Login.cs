﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CourseManagementAPI.AutoGeneratedModel
{
    public partial class Login
    {
        public string LoginId { get; set; }
        public int CustomerId { get; set; }
        public string PasswordHash { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
