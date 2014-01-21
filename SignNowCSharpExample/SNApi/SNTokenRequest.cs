using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignNowCSharpExample.SNApi
{
    public class SNTokenRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
        public string scope { get; set; }
    }
}