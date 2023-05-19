using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetModels
{
    public class Commenti
    {
        public string IdUtente { get; set; }
        public string NomeUtente { get; set; }
        public string Commento { get; set; }
        public List<Like> Likes { get; set; }
        public List<Commenti> Risposte { get; set; }
    }

}
