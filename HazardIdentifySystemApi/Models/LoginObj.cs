using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Models
{
    public class LoginObj
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { set; get; }
    }
}