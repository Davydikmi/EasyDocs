using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Net;

namespace EasyDocs
{
    public class ClientData
    {
        [JsonProperty("FIO")]
        public string FIO { get; set; }

        [JsonProperty("phone_number")]
        public string phone_numb { get; set; }
        [JsonProperty("birthDate")]
        public string birth_date { get; set; }

        [JsonProperty("passportSeriesNumb")]
        public string passport_SeriesNumb { get; set; }

        [JsonProperty("adress")]
        public string adress { get; set; }

        [JsonProperty("id_number")]
        public string id_numb { get; set; }
        private const string filepath = "ClientData.json";

        public void AddClientToJson(ClientData newClient)
        {

        }
    }
}