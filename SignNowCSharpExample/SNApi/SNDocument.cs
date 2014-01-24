using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignNowCSharpExample.SNApi
{
    public class SNDocument
    {
        public int id { get; set; }
        public string filename { get; set; }

        public string access_token { get; set; }
    }
}