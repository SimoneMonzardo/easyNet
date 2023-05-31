using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Models.ModelVM
{
    public class FollowerVM
    {
        public string username { get; set; }
        public string profilePicture { get; set; }
        public string companyName { get; set; }
        public bool followed { get; set; }
    }
}
