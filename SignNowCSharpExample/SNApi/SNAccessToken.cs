﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignNowCSharpExample.SNApi
{
    public class SNAccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string id { get; set; }
        public string scope { get; set; }
    }
}