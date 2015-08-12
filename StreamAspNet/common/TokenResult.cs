using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamAspNet.Common
{
    public class TokenResult
    {
        public string message { get; set; }
        public string token { get; set; }
        public bool success { get; set; }
    }
}