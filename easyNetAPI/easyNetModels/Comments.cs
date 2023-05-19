using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetModels
{
    public class Comments
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comments> Answers { get; set; }
    }

}