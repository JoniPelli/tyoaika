using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace tyoaika
{
    internal class Tyoaika
    {
        public string stringPvm { get; set; }
        public DateTime Pvm { get; set; }
        public string Tunnit { get; set; }
        public int TehtavaID { get; set; }
        public string Tehtava { get; set; }
        public int KohdeID { get; set; }
        public string Kohde { get; set; }
        public string Vapaateksti { get; set; }
        public void paivaysMerkkijoniksi()
        {
            stringPvm = Pvm.Day + "." + Pvm.Month + "." + Pvm.Year;
        }
    }
}
