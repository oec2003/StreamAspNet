using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StreamAspNet.Common
{

    public class UploadResult
    {
        public string message { get; set; }
        public long start { get; set; }
        public bool success { get; set; }
    }
}