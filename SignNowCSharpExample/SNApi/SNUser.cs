﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignNowCSharpExample.SNApi
{
    public class SNUser
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }

    }
}