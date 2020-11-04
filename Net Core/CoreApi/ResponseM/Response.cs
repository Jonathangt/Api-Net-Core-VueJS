using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.ResponseM
{
    [JsonObject]
    public class Response
    {
        [JsonProperty("codigo")]
        public int CodTransaccion { get; set; }

        [JsonProperty("mensaje")]
        public string MsgTransaccion { get; set; }

      
    }
}
