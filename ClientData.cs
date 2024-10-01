using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDocs
{
    public class ClientData
    {
        public string FIO {  get; set; }
        public string short_FIO { get; set; }
        public string phone_numb { get; set; }
        public DateTime birth_date { get; set; }
        public string docType_seriesNumb { get; set; } // Паспорт + KH1234567
        public string adress { get; set; }
        public string id_numb { get; set; }
    }
}