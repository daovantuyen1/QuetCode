using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models.Account
{
    public class CheckLoginRequest
    {
        public string UserName { set; get; }
        public string PassWord { set; get; }
    }
}